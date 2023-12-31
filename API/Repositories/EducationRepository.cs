﻿using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    //class child hasil inheritance/pewarisan dari class Generalrepository
    public class EducationRepository : GeneralRepository<Education>, IEducationyRepository
    {
        public EducationRepository(BookingManagementDbContext context) : base(context) { }

    }
}
