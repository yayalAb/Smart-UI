using Application.Common.Mappings;
using Application.GoodModule.Queries;
using Application.GoodModule.Queries.GoodByContainer;
using Domain.Entities;

namespace Application.Common.Service
{
    public class GeneratedDocumentsGoodsDto : IMapFrom<GeneratedDocumentGood>
    {
        public int Quantity { get; set; }
        public GoodByContainerDto Good { get; set; }
    }
}