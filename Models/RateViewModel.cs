using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;

namespace Evention2.Models
{
    /**
     * Model using in posting a review.
     */
    public class RateCreateViewModel
    {
        public int Event_id { get; set; }
        public string Event_name { get; set; }
        public int Id { get; set; }
        [Range(1, 5)]
        public int Rating_Score { get; set; }
        [StringLength(64)]
        public string Comments { get; set; }

        /**
         * Overriden of ToString
         */
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Event ID: " + Event_id + ", ");
            builder.Append("Rating Score: " + Rating_Score + ", ");
            builder.Append("Comments: " + Comments);
            return builder.ToString();
        }

        /**
         * Convert to Rate.
         */
        public Rate ToRate()
        {
            Rate rate = new Rate();
            rate.Event_id = this.Event_id;
            rate.Rating_Score = this.Rating_Score;
            rate.Comments = this.Comments;
            return rate;
        }
    }
}