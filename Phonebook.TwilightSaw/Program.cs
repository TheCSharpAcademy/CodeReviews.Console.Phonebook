using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using TwilightSaw.Phonebook.Controller;
using TwilightSaw.Phonebook.Factory;
using TwilightSaw.Phonebook.View;


ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
Console.Title = "Phonebook by TwilightSaw";
var app = HostFactory.CreateDbHost(args);

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
context.Database.Migrate();

var controller = new ContactController(context);
var emailController = new EmailController();
var messageController = new MessageController();
var categoryController = new CategoryController(context);
new MainMenu(controller, emailController, categoryController, messageController).Menu();

