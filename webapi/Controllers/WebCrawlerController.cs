using Microsoft.AspNetCore.Mvc;
using HtmlAgilityPack;

namespace webapi.Controllers
{
    [Route("crawler")]
    [ApiController]
    public class WebCrawlerController : Controller
    {

        [HttpGet("ProductInfo")]
        public async Task<IActionResult> GetProductInformation([FromQuery] string url)
        {
            try
            {
                if (string.IsNullOrEmpty(url))
                {
                    return BadRequest("URL is required.");
                }

                string html = await DownloadHtml(url);

                if (!string.IsNullOrEmpty(html))
                {
                    var document = new HtmlDocument();
                    document.LoadHtml(html);

                    // Extracting product information
                    string productName = GetProductName(document);
                    string productPrice = GetProductPrice(document);

                    var productInfo = new
                    {
                        Name = productName,
                        Price = productPrice
                    };

                    return Ok(new { html, productInfo});
                }
                else
                {
                    return BadRequest("Failed to download HTML from the provided URL.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        private async Task<string> DownloadHtml(string url)
        {
            using (var client = new HttpClient())
            {
                return await client.GetStringAsync(url);
            }
        }

        private string GetProductName(HtmlDocument document)
        {
            var nameNode = document.DocumentNode.SelectSingleNode("//span[@id='productTitle']");
            return nameNode?.InnerText.Trim() ?? "N/A";
        }

        private string GetProductPrice(HtmlDocument document)
        {
            var priceNode = document.DocumentNode.SelectSingleNode("//span[@id='priceblock_ourprice']");
            return priceNode?.InnerText.Trim() ?? "N/A";
        }
    }
}
