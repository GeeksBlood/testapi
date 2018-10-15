using Aguila.DAL;
using Aguila.Models;
using AutoMapper;

namespace Aguila.Service
{
    /// <summary>
    /// This is Mapping class of Automatpper and inheriting the Profile class
    /// Here we maps all our Dto's with the entity class of DAL
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Map the objects
            CreateMap<AnswersDto, Answers>().ReverseMap();
            CreateMap<AttributesDto, Attributes>().ReverseMap();
            CreateMap<PictureBooksDto, PictureBooks>().ReverseMap();
            CreateMap<HandicapsDto, Handicaps>().ReverseMap();
            CreateMap<UserRoundCompletedsDto, UserRoundCompleteds>().ReverseMap();
            CreateMap<LrdattributesDto, Lrdattributes>().ReverseMap();
            CreateMap<LrddatasDto, Lrddatas>().ReverseMap();
            CreateMap<LrdcategoriesDto, Lrdcategories>().ReverseMap();
            CreateMap<LrdattributesLookupsDto, LrdattributesLookups>().ReverseMap();
            CreateMap<SituationCategoriesDto, SituationCategories>().ReverseMap();
            CreateMap<ApplicationUserDto, AspNetUsers>().ReverseMap();
            CreateMap<UserDto, AspNetUsers>().ReverseMap();
            CreateMap<SituationAttributesDto, SituationAttributes>().ReverseMap();
            CreateMap<SituationHandicapsDto, SituationHandicaps>().ReverseMap();
            CreateMap<SituationQuestionsDto, SituationQuestions>().ReverseMap();
            CreateMap<SituationCategoriesDto, SituationCategories>().ReverseMap();
            CreateMap<SituationsDto, Situations>().ReverseMap();
            CreateMap<SituationsByHoleDto, Situations>().ReverseMap();
            CreateMap<SituationHoleDto, Situations>().ReverseMap();
            CreateMap<CategoryAttributesDto, CategoryAttributes>().ReverseMap();

            CreateMap<NewSituationViewDto, Situations>().ReverseMap();
            CreateMap<NewSituationAttributesDto, SituationAttributes>().ReverseMap();
            CreateMap<NewSituationHandicapDto, SituationHandicaps>().ReverseMap();
            CreateMap<NewSituationQuestionsDto, SituationQuestions>().ReverseMap();
            CreateMap<NewPictureBookDto, PictureBooks>().ReverseMap();
            CreateMap<PictureBooksAdminDto, PictureBooks>().ReverseMap();
            CreateMap<UserRoundCompletedQuestionsDto, UserRoundCompletedQuestions>().ReverseMap();
        }
    }
}