using BucketMgmt_AWS_S3.Aplication.Bucket;
using BucketMgmt_AWS_S3.Aplication.File;
using BucketMgmt_AWS_S3.Infra;
using Microsoft.OpenApi.Models;
using S3ManagementAPI.Domain;
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;

namespace BucketMgmt_AWS_S3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "AWS S3 Management API",
                    Version = "v1",
                    Description = "C# API to Manage your S3 buckets and files",
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            }
            );


            builder.Services.AddScoped<IAwsClient, AwsClient>();
            builder.Services.AddScoped<IFilesService, FilesService>();
            builder.Services.AddScoped<IBucketService, BucketService>();

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