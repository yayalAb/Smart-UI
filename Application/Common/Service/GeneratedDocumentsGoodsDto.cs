using Application.Common.Mappings;
using Application.GoodModule.Queries;
using Domain.Entities;

namespace Application.Common.Service
{
    public class GeneratedDocumentsGoodsDto : IMapFrom<GeneratedDocument>
    {
        public int Quantity { get; set; }
        public FetchGoodDto Good { get; set; }
    }
}