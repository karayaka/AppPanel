using AppPanel.DAL.Classes.EnglishQuizerClasses;
using AppPanel.Sevices.Models.QuizerModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppPanel.Sevices.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ///CreateMap<Personel, VMPersonel>();
            //level models
            CreateMap<Level, LevelModel>();
            CreateMap<LevelModel,Level>();

            //topicModels
            CreateMap<TopicModel, Topic>();
            CreateMap<Topic,TopicModel>();

            //test model
            CreateMap<Test,TestModel>();
            CreateMap<TestModel,Test>();

            //question model
            CreateMap<Question, QuestionModel>();
            CreateMap<QuestionModel, Question>();

        }
    }
}
