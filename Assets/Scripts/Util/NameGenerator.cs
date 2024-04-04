
using UnityEngine;

namespace Util
{
    public class NameGenerator
    {
        public static string[] names =
        {
            "George", "Andy","Clara","Jason","April","Hanna","Julia","Bob","William","David","Angelo","Jesus","Chris","Emily",
            "Peter","Jackson","Jacob","Bella","Stephen","Penelope","Lorene"
        };

        public static string getRandomName() => names[Random.Range(0, names.Length)];
        
    }
}