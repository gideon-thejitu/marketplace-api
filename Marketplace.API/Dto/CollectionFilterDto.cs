using System.ComponentModel;

namespace Marketplace.Dto;

public class CollectionFilterDto
{
    [DefaultValue(1)]
    public int Page { get; set; }
    
    [DefaultValue(100)]
    public int Limit { get; set; }

    protected CollectionFilterDto()
    {
        Page = Page > 0 ? Page : 1;
        Limit = Limit > 0 ? Limit : 100;
    }
}
