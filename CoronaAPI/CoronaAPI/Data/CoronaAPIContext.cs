using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CoronaAPI.Models;

namespace CoronaAPI.Data
{
    public class CoronaAPIContext : DbContext
    {
        public CoronaAPIContext (DbContextOptions<CoronaAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; } = default!;
        public DbSet<PatientInfection> PatientInfections { get; set; } = default!;
        public DbSet<PatientVaccination> PatientVaccinations { get; set; } = default!;
        public DbSet<City> Cities { get; set; } = default!;
        public DbSet<Manufactor> Manufactors { get; set; } = default!;
    }
}
