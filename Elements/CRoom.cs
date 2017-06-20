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
        private List<CServerClient> clients;
        private List<CRoom> rooms;
        
        private CRoom defaultRoom;
        
        // For server usage.
        public CChatRooms(List<CServerClient> _clients) {
            this.clients = _clients;
            this.rooms = new List<CRoom>();

            this.defaultRoom = AddRoom("home");
        }
        
        public CRoom AddRoom(string name) {
            if(GetRoom(name) != null)
                return null;
                
            // Adding a new room.
            CRoom room = new CRoom(name);
            rooms.Add(room);
            
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
        }
        
        public void MoveToRoom(CUser user, string name) {
            // Logic for moving a user from one room to another.
            // Basic procedure is as follows;
            // Announce leave to current room, announce arrival to destination room,
            // set user.Room to new room.
            
            CRoom target = null,
                  old = user.Room;
            if((target = GetRoom(name)) == null)
                return;

            if(old != null)
                old.RemoveUser(user);
            target.AddUser(user);

            Console.WriteLine(user.Username + " was moved to '" + target.Name + "'");
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
        public string Name { get { return this.sName; } }
        public bool Muted { get { return this.bMutedRoom; } }
        
        private string sName;
        private bool bMutedRoom = false; // Set to true, no messages will be sent to any user.
        
        private List<CUser> users;
        
        // For server usage.
        public CRoom(string _name) {
            this.sName = _name;
            this.users = new List<CUser>();
        }
        
        // For client usage (storage)
        public CRoom(string _name, bool _muted) {
            this.sName = _name;
            this.bMutedRoom = _muted;
        }
        
        public void AddUser(CUser user) {
            if(HasUser(user))
                return;

            user.UpdateRoom(this);
            users.Add(user);
        }

        public void RemoveUser(CUser user) {
            if(!HasUser(user))
                return;
                
            // Send announcement.
            users.Remove(user);
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
