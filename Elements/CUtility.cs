using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRat.Elements {
    // Stateless macros.
    public class CUtility {
        public static bool IsBlank(string src) {
            if (src.Length == 0 || src == "")
                return true;

            for(int i = 0; i < src.Length; i++) {
                if (src[i] != ' ')
                    return false;
            }
            return true;
        }
    }
}
