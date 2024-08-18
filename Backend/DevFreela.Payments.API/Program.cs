using MediatR;
using DevFreela.Payments.API.Commands;
using DevFreela.Payments.API.Services;
using DevFreela.Payments.API.Consumers;

namespace DevFreela.Payments.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddHostedService<ProcessPaymentConsumer>();

            builder.Services.AddControllers();
            builder.Services.AddMediatR(typeof(PaymentCommand));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
