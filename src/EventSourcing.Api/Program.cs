
using EventSourcing.Api.Aggregates.Model;
using FluentValidation.AspNetCore;
using FluentValidation;
using MediatR;
using Marten.Events.Daemon.Resiliency;
using Marten;
using Marten.Events.Projections;
using Weasel.Core;
using EventSourcing.Api.Aggregates.MartenDb.Repository;

namespace EventSourcing.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            //Fluent Validation
            //builder.Services.AddFluentValidationAutoValidation();
            //builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //MediatR
            builder.Services.AddMediatR(typeof(Program));

            //MartenDB
            builder.Services.AddMarten(opt =>
            {
                var connString = builder.Configuration.GetConnectionString("Postgre");

                opt.UseDefaultSerialization(EnumStorage.AsString, nonPublicMembersStorage: NonPublicMembersStorage.All);

                opt.Connection(connString);

                opt.DatabaseSchemaName = "eventsourcing";

                opt.Projections.SelfAggregate<Account>(ProjectionLifecycle.Live);
            });

            //Repositories
            builder.Services.AddScoped<IMartenRepository<Account>, MartenRepository<Account>>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}