using AutoMapper;
using Microsoft.Practices.Unity;
using Pattern.Common.Contract;
using Pattern.DataContext.Entities;
using Pattern.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pattern.Services.Configuration
{
    public class MapperProfile : Profile
    {
        [Dependency]
        protected IUserProfile UserProfile { get; set; }

        public MapperProfile()
        {
            CreateMap<Widget, WidgetModel>();
        }

        //public class CombinedAddressMemberResolver : IMemberValueResolver<Patient, CombinedReferralPatientModel, ICollection<PatientAddress>, CombinedReferralPatientAddressModel>
        //{
        //    public CombinedReferralPatientAddressModel Resolve(Patient source, CombinedReferralPatientModel destination, ICollection<PatientAddress> input, CombinedReferralPatientAddressModel destMember, ResolutionContext ctx)
        //    {
        //        CombinedReferralPatientAddressModel ret = new CombinedReferralPatientAddressModel();
        //        var a = input.FirstOrDefault();
        //        if (a != null)
        //        {
        //            ret = Mapper.Map<CombinedReferralPatientAddressModel>(a);
        //            return ret;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}
    }
}
