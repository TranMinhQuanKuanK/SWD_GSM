using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.StoreOwner;
using BusinessLayer.Interfaces.SystemAdmin;
using BusinessLayer.Services;
using BusinessLayer.Services.StoreOwner;
using BusinessLayer.Services.SystemAdmin;
using DataAcessLayer;
using DataAcessLayer.Interfaces;
using DataAcessLayer.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Utilities;

namespace SWD_GSM
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddRouting(option =>
            {
                option.ConstraintMap["slugify"] = typeof(SlugifyParameterTransformer);
            });
            services.AddDbContext<GroceryCloudContext>(
               options => options.UseSqlServer(Configuration.GetConnectionString("GroceryCloud")));

            services.AddControllers();
            services.AddControllers(options =>
            {
                options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
            });

            services.AddSwaggerGen(options =>
            {

                options.SwaggerDoc("v1", new OpenApiInfo { Title = "SWD_GSM", Version = "v1" });
                options.DocumentFilter<KebabCaseDocumentFilter>();

                options.TagActionsBy(api =>
                {
                    var controllerActionDescriptor = api.ActionDescriptor as ControllerActionDescriptor;
                    string controllerName = controllerActionDescriptor.ControllerName;

                    if (api.GroupName != null)
                    {
                        var name = api.GroupName + controllerName.Replace("Controller", "");
                        name = Regex.Replace(name, "([a-z])([A-Z])", "$1 $2");
                        return new[] { name };
                    }

                    if (controllerActionDescriptor != null)
                    {
                        controllerName = Regex.Replace(controllerName, "([a-z])([A-Z])", "$1 $2");
                        return new[] { controllerName };
                    }

                    throw new InvalidOperationException("Unable to determine tag for endpoint.");
                });
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                            {
                                { jwtSecurityScheme, Array.Empty<string>() }
                            });
                //options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                //{
                //    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                //    Name = "Authorization",
                //    In = ParameterLocation.Header,
                //    Type = SecuritySchemeType.ApiKey,
                //    Scheme = "Bearer"
                //});

                //options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                //{
                //        {
                //            new OpenApiSecurityScheme
                //            {
                //                Reference = new OpenApiReference
                //                {
                //                    Type = ReferenceType.SecurityScheme,
                //                    Id = "Bearer"
                //                },
                //                Scheme = "oauth2",
                //                Name = "Bearer",
                //                In = ParameterLocation.Header,

                //            },
                //            new List<string>()
                //        }
                //});
                options.DocInclusionPredicate((name, api) => true);
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
          .AddJwtBearer(options =>
          {
              options.TokenValidationParameters = new TokenValidationParameters
              {
                  ValidateIssuer = true,
                  ValidateAudience = true,
                  ValidateLifetime = true,
                  ValidateIssuerSigningKey = true,

                  ValidIssuer = "http://localhost:2000",
                  ValidAudience = "http://localhost:2000",
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KeyForSignInSecret@1234"))
              };
          });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //admin
            services.AddTransient<BusinessLayer.Interfaces.SystemAdmin.IBrandService,
                 BusinessLayer.Services.SystemAdmin.BrandService>();
            services.AddTransient<BusinessLayer.Interfaces.SystemAdmin.IStoreService,
                BusinessLayer.Services.SystemAdmin.StoreService>();
            services.AddTransient<IUserService, UserService>();
            //storeowner
            services.AddTransient<BusinessLayer.Interfaces.StoreOwner.IProductService,
                 BusinessLayer.Services.StoreOwner.ProductService>();
            services.AddTransient<BusinessLayer.Interfaces.StoreOwner.ICategoryService,
                 BusinessLayer.Services.StoreOwner.CategoryService>();
            services.AddTransient<BusinessLayer.Interfaces.StoreOwner.IBillService,
                 BusinessLayer.Services.StoreOwner.BillService>();
            services.AddTransient<BusinessLayer.Interfaces.StoreOwner.IReceiptService,
                 BusinessLayer.Services.StoreOwner.ReceiptService>();
            services.AddTransient<BusinessLayer.Interfaces.StoreOwner.IBrandService,
                BusinessLayer.Services.StoreOwner.BrandService>();
            services.AddTransient<BusinessLayer.Interfaces.StoreOwner.IStoreService,
                BusinessLayer.Services.StoreOwner.StoreService>();
            services.AddTransient<IDailyRevenueService, DailyRevenueService>();
            services.AddTransient<IEventService, EventService>();
            services.AddTransient<IStockService, StockService>();
            services.AddTransient<ICashierService, CashierSevice>();
            //cashier
            services.AddTransient<BusinessLayer.Interfaces.Cashier.IProductService,
                 BusinessLayer.Services.Cashier.ProductService>();
            services.AddTransient<BusinessLayer.Interfaces.Cashier.ICategoryService,
                 BusinessLayer.Services.Cashier.CategoryService>();
            services.AddTransient<BusinessLayer.Interfaces.Cashier.IBillService,
                BusinessLayer.Services.Cashier.BillService>();
            services.AddTransient<BusinessLayer.Interfaces.Cashier.IReceiptService,
                 BusinessLayer.Services.Cashier.ReceiptService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SWD_GSM v1"));
            }
            app.UseRewriter(new RewriteOptions().Add(new PascalRule()));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
