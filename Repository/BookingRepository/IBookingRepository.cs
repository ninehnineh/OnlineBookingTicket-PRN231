﻿using BusinessObject.Entities;
using DTO.Booking;
using Repository.ServiceResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.BookingRepository
{
    public interface IBookingRepository
    {
        Task<CreateBookingResponse> BookingAsync(CreateBookingDto bookingDto);
        Task<BookingDto> GetBookingAsync(string userId);
        Task<BookingDto> GetBookingByUserIdAndMovieShowIdAsync(string userId, int movieShowId);
        Task<List<BookingDto>> GetBookingsAsync(string userId);
    }
}
