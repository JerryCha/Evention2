using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Evention2.Services
{

    public class PosterManagementService
    {
        // Disable constructing from outside class.
        private PosterManagementService() { }

        private static PosterManagementService instance = new PosterManagementService();

        private Dictionary<String, String> TempStorage;

        public static PosterManagementService GetInstance()
        {
            if (instance == null)
            {
                instance = new PosterManagementService();
            }
            return instance;
        }

        /**
         * Add a poster for a user. Only one poster per user could temporary storage.
         */
        public bool AddPoster(string userId, String filePath)
        {
            TempStorage[userId] = filePath;
            return true;
        }

        public string PopPoster(string userId)
        {
            string filePath;
            TempStorage.TryGetValue(userId, out filePath);
            TempStorage.Remove(userId);
            return filePath;
        }

        public bool HasPoster(string userId)
        {
            return TempStorage.ContainsKey(userId);
        }
    }
}