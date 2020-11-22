using BusinessLogic.Contract;
using Moq;
using ResourceAccess.Engines;
using ResourceAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test
{

    public class ResourceMunicipalityTaxScheduleEngineMock : IResourceMunicipalityTaxScheduleEngine
    {

        private IEnumerable<IMunicipalityTaxScheduleModel> municipalityTaxSchedules = new List<IMunicipalityTaxScheduleModel>
       {
              new MunicipalityTaxScheduleDbModel { Municipality ="Copenhagen", Tax =0.2M,TaxScheduleType=TaxScheduleType.YEAR, ValidFrom = new DateTime(2016,1,1), ValidTo = new DateTime(2016,12,31) },
              new MunicipalityTaxScheduleDbModel { Municipality ="Copenhagen", Tax =0.4M,TaxScheduleType=TaxScheduleType.MONTH, ValidFrom = new DateTime(2016,5,1), ValidTo = new DateTime(2016,5,31) },
              new MunicipalityTaxScheduleDbModel { Municipality ="Copenhagen", Tax =0.1M,TaxScheduleType=TaxScheduleType.DAY, ValidFrom = new DateTime(2016,1,1), ValidTo = new DateTime(2016,1,1) },
              new MunicipalityTaxScheduleDbModel { Municipality ="Copenhagen", Tax =0.1M,TaxScheduleType=TaxScheduleType.DAY, ValidFrom = new DateTime(2016,12,25), ValidTo = new DateTime(2016,12,25) },
             new MunicipalityTaxScheduleDbModel { Municipality ="Vaerloese", Tax =0.1M,TaxScheduleType=TaxScheduleType.YEAR, ValidFrom = new DateTime(2020,1,1), ValidTo = new DateTime(2020,12,31) },
              new MunicipalityTaxScheduleDbModel { Municipality ="Vaerloese", Tax =0.2M,TaxScheduleType=TaxScheduleType.MONTH, ValidFrom = new DateTime(2020,11,1), ValidTo = new DateTime(2020,11,30) },
              new MunicipalityTaxScheduleDbModel { Municipality ="Vaerloese", Tax =0.3M,TaxScheduleType=TaxScheduleType.WEEK, ValidFrom = new DateTime(2020,11,23), ValidTo = new DateTime(2020,11,29) },
              new MunicipalityTaxScheduleDbModel { Municipality ="Vaerloese", Tax =0.4M,TaxScheduleType=TaxScheduleType.DAY, ValidFrom = new DateTime(2020,11,25), ValidTo = new DateTime(2020,11,25) },

        };

        protected Mock<ResourceMunicipalityTaxScheduleEngineMock> mock;


        public ResourceMunicipalityTaxScheduleEngineMock()
        {
            mock = new Mock<ResourceMunicipalityTaxScheduleEngineMock>(MockBehavior.Default);

            mock.Setup(engine => engine.UpdateMunicipalityTaxSchedule(It.IsAny<IUpdateMunicipalityTaxScheduleRequest>()));
            

            mock.Setup(engine => engine.RetríeveMunicipalityTaxSchedules(It.IsAny<IRetríeveMunicipalityTaxSchedulesRequest>()))
                .ReturnsAsync((IRetríeveMunicipalityTaxSchedulesRequest request) =>
                {

                    return new RetríeveMunicipalityTaxSchedulesResponse
                    {
                        TaxSchedules = municipalityTaxSchedules.Select(s => s).Where(s =>
                            s.Municipality.Equals(request.Municipality) &&
                            s.ValidFrom <= request.TaxDate &&
                            s.ValidTo >= request.TaxDate)
                    };

                });

         
        }

        public virtual async Task<IRetríeveMunicipalityTaxSchedulesResponse> RetríeveMunicipalityTaxSchedules(IRetríeveMunicipalityTaxSchedulesRequest request)
        {
            return await mock.Object.RetríeveMunicipalityTaxSchedules(request);
        }

        public virtual async Task UpdateMunicipalityTaxSchedule(IUpdateMunicipalityTaxScheduleRequest request)
        {
            await mock.Object.UpdateMunicipalityTaxSchedule(request);
        }
    }
}

