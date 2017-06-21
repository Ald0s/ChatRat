/*
== ChatRat ==
A basic TCP application built around my networking library.

By Alden Viljoen
https://github.com/ald0s

== Summary ==
A class responsible for coordinating chat rooms, and the actual
room class.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChatRat.Network.Messages;
using ChatRat.Network.Server;
using ZapNetwork.Server;

namespace ChatRat.Elements {
    public class CChatRooms {
        public List<CRoom> Rooms { get { return this.rooms; } }

        private List<CServerClient> clients;
        private List<CRoom> rooms;

        public delegate void RoomAdded_Delegate(CRoom newRoom);
        public event RoomAdded_Delegate RoomAdded;

        public delegate void RoomRemoved_Delegate(CRoom oldRoom);
        public event RoomRemoved_Delegate RoomRemoved;
        
        private CRoom defaultRoom;
        
        // For server usage.
        public CChatRooms(List<CServerClient> _clients) {
            this.clients = _clients;
            this.rooms = new List<CRoom>();

            this.defaultRoom = AddRoom("Home", "home");
            AddRoom("Politics", "pol");
        }
        
        public CRoom AddRoom(string display_name, string name) {
            if(GetRoom(name) != null)
                return null;
                
            // Adding a new room.
            CRoom room = new CRoom(name, display_name);
            rooms.Add(room);

            if (RoomAdded != null)
                RoomAdded(room);
            
            return room;
        }
        
        public void RemoveRoom(string name) {
            // Removing a room from the choices.
            // This will mirror to all users in the room.
            CRoom target = null;
            if((target = GetRoom(name)) == null)
                return;

            // Mute the room so users don't get spammed with announcements of other room
            // users being moved.
            target.Mute(true);

            // Relocate all users in this room to home.
            for (int j = 0; j < clients.Count; j++) {
                if(((CUser)clients[j]).Room.Name == target.Name) {
                    MoveToRoom(((CUser)clients[j]), "home"); 
                }
            }
            
            rooms.Remove(target);

            // Report to all UI systems that the room has been removed.
            // Server and client.
            if (RoomRemoved != null)
                RoomRemoved(target);
        }
        
        public bool MoveToRoom(CUser user, string name) {
            // Logic for moving a user from one room to another.
            // Basic procedure is as follows;
            // Announce leave to current room, announce arrival to destination room,
            // set user.Room to new room.
            
            CRoom target = null,
                  old = user.Room;
            if((target = GetRoom(name)) == null)
                return false;

            if(old != null)
                old.RemoveUser(user);
            if (!target.AddUser(user, !(old == null)))
                return false;

            Console.WriteLine(user.Username + " was moved to '" + target.Name + "'");

            return true;
        }
        
        private CRoom GetRoom(string name) {
            for(int i = 0; i < rooms.Count; i++) {
                if(rooms[i].Name == name)
                    return rooms[i];
            }
            return null;
        }
    }
    
    public class CRoom {
        public string DisplayName { get { return this.sDisplayName; } }
        public string Name { get { return this.sName; } }
        public bool Muted { get { return this.bMutedRoom; } }
        public bool Locked { get { return this.bLocked; } }

        private string sDisplayName; // A friendly version of name.
        private string sName;
        private bool bMutedRoom = false; // Set to true, no messages will be sent to any user.
        private bool bLocked = false; // Can this room be removed?
        
        [NonSerialized]
        private List<CUser> users;
        
        // For server usage.
        public CRoom(string _name, string _disname, bool _locked = false) {
            this.sName = _name;
            this.sDisplayName = _disname;
            this.bLocked = _locked;

            this.users = new List<CUser>();
        }
        
        // For client usage (storage)
        public CRoom(string _name, string _disname, bool _muted, bool _locked = false) {
            this.sName = _name;
            this.sDisplayName = _disname;
            this.bLocked = _locked;
            this.bMutedRoom = _muted;
        }
        
        public bool AddUser(CUser user, bool broadcast = true) {
            if(HasUser(user))
                return false;

            // Do any authentication checks here.
            // Like, does the rank allow this user to join this group?
            // At this time, just succeed.
            if(broadcast)
                user.SendNetMessage(new msg_ChangeRoomResult(this, true));

            // Broadcast this user to THIS room as arriving.
            if(broadcast)
                this.BroadcastMessage(new msg_JoinLeave(user, this, true));

            user.UpdateRoom(this);
            users.Add(user);

            return true;
        }

        public void RemoveUser(CUser user, bool broadcast = true) {
            if(!HasUser(user))
                return;

            users.Remove(user);

            // Broadcast this user has left to their CURRENT room.
            if(broadcast)
                user.Room.BroadcastMessage(new msg_JoinLeave(user, user.Room, false));
        }
        
        public bool HasUser(CUser user) {
            return users.Contains(user);
        }
        
        public void Mute(bool shouldMute) {
            this.bMutedRoom = shouldMute;
        }
        
        public void ChatMessage(msg_SendMessage msg) {
            BroadcastMessage(msg);
        }
        
        private void BroadcastMessage(CRatMessage msg) {
            if(bMutedRoom)
                return;
            
            for(int i = 0; i < users.Count; i++) {
                users[i].SendNetMessage(msg);
            }
        }
    }
}
