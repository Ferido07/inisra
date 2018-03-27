using AutoMapper;
using Inisra_Web_App_MVC.DTOs;
using Inisra_Web_App_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inisra_Web_App_MVC
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Job, JobDto>()
                .ForMember(dto => dto.Rate, opt => {
                  opt.MapFrom(job => job.SalaryRate.ToString());
                  
                    // opt.ResolveUsing<RateResolver>();
                }
                )
            );
        }
         
    }

    public class RateResolver : IValueResolver<Job, JobDto, string>
    {
        public string Resolve(Job source, JobDto destination, string destMember, ResolutionContext context)
        {
            switch (source.SalaryRate)
            {
                case Rate.Hourly : return "Hourly";
                case Rate.Monthly: return "Monthly";
                case Rate.Weekly : return "Weeekly";
                case Rate.Yearly : return "Yearly";
                default          : return "Monthly";
            }
        }
    }
}