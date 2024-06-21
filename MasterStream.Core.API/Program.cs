//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using MasterStream.Core.API.Brokers.Blobs;
using MasterStream.Core.API.Brokers.DateTimes;
using MasterStream.Core.API.Brokers.Loggings;
using MasterStream.Core.API.Models.VideoMetadatas.Brokers.Storages;
using MasterStream.Core.API.Services.VideoMetadatas;
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<StorageBroker>();
        builder.Services.AddTransient<IStorageBroker, StorageBroker>();
        builder.Services.AddTransient<IBlobBroker, BlobBroker>();
        builder.Services.AddTransient<ILoggingBroker, LoggingBroker>();
        builder.Services.AddTransient<IDateTimeBroker, DateTimeBroker>();
        builder.Services.AddTransient<IVideoMetadataService, VideoMetadataService>();
        var app = builder.Build();

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