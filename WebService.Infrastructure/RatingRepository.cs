﻿namespace WebService.Infrastructure
{
    public class RatingRepository : IRatingRepository
    {
        private readonly IContext _context;
        public RatingRepository(IContext context)
        {
            _context = context;
        }

        public Task<(Status, RatingDTO)> CreateAsync(CreateRatingDTO rating)
        {
            throw new NotImplementedException();
        }

        public Task<Status> DeleteAsync(int ratingId)
        {
            throw new NotImplementedException();
        }

        public Task<(Status, RatingDTO)> ReadAsync(int ratingId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<RatingDTO>> ReadAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Status> UpdateAsync(RatingDTO ratingDTO)
        {
            throw new NotImplementedException();
        }
    }
}
