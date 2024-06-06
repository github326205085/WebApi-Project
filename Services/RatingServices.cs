using Entities;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RatingServices:IRatingServices
    {
        private readonly IRatingRepository ratingRepository;

        public RatingServices(IRatingRepository ratingRepository)
        {
            this.ratingRepository = ratingRepository;


        }

        public async Task<Rating> AddRating(Rating rating)
        {
            return await ratingRepository.AddRating(rating);
        }


    }
}
