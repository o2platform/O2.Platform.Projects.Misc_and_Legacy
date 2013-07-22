using FluentSharp.CoreLib;
using mshtml;
using O2.External.IE.Interfaces;

namespace O2.External.IE.WebObjects
{
    public class IE_Img : IO2HtmlImg
    {
        public string OuterHtml { get; set; }

         public IE_Img(object _object)
        {
        	if (_object is DispHTMLImg)
				loadData((DispHTMLImg)_object);
			else
				"In IE_Img, not supported type: {0}".format(_object.comObject_TypeName()).error();
		}
		
        public IE_Img(DispHTMLImg image)
        {
            loadData(image);
        }
        
        public void loadData(DispHTMLImg image)        
        {
            OuterHtml = image.outerHTML;
        }
    }
}