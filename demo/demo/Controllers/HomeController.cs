using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;

namespace demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacebookController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly string _accessToken = "EAAN4ABpkgZCQBRD13HhWDp2VaZCK5MzsFviHL32kOdwLjMzOdoaMhMgy68HIMyLduweZAc7ZACElcH6GItX9ZBD1zGX9IZBZAIZCfIZBZAsSnHGEJi9btpGq1WJJWzh35oPmZC8CcXGBHYJD4a1Ank55beKKZBnNDQJTeQYtDJtZARu40LtkmL1ngTDnMs47ZA3U8IJ6ohOwvgW5sdFTufXuBbcA99A5OXZAfSadM9ti9hbeR4xvcPw";
        private readonly string _fbBaseUrl = "https://graph.facebook.com/v20.0";

        public FacebookController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // 1. GET /api/page/{pageId} - Lấy thông tin Page
        [HttpGet("page/{pageId}")]
        public async Task<IActionResult> GetPageInfo(string pageId)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_fbBaseUrl}/{pageId}?fields=name,about,fan_count&access_token={_accessToken}");

            if (!response.IsSuccessStatusCode) return BadRequest(await response.Content.ReadAsStringAsync());

            var content = await response.Content.ReadAsStringAsync();
            return Ok(JsonConvert.DeserializeObject(content));
        }

        // 2. GET /api/page/{pageId}/posts - Lấy danh sách bài viết
        [HttpGet("page/{pageId}/posts")]
        public async Task<IActionResult> GetPagePosts(string pageId)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_fbBaseUrl}/{pageId}/feed?access_token={_accessToken}");

            var content = await response.Content.ReadAsStringAsync();
            return Ok(JsonConvert.DeserializeObject(content));
        }

        // 3. POST /api/page/{pageId}/posts - Đăng bài viết mới
        [HttpPost("page/{pageId}/posts")]
        public async Task<IActionResult> CreatePost(string pageId, [FromBody] PostRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            var url = $"{_fbBaseUrl}/{pageId}/feed";

            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("message", request.Message),
                new KeyValuePair<string, string>("access_token", _accessToken)
            });

            var response = await client.PostAsync(url, data);
            var content = await response.Content.ReadAsStringAsync();
            return Ok(JsonConvert.DeserializeObject(content));
        }

        // 4. DELETE /api/page/post/{postId} - Xóa bài viết
        [HttpDelete("page/post/{postId}")]
        public async Task<IActionResult> DeletePost(string postId)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"{_fbBaseUrl}/{postId}?access_token={_accessToken}");

            var content = await response.Content.ReadAsStringAsync();
            return Ok(JsonConvert.DeserializeObject(content));
        }

        // 5. GET /api/page/post/{postId}/comments - Lấy danh sách bình luận
        [HttpGet("page/post/{postId}/comments")]
        public async Task<IActionResult> GetComments(string postId)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_fbBaseUrl}/{postId}/comments?access_token={_accessToken}");

            var content = await response.Content.ReadAsStringAsync();
            return Ok(JsonConvert.DeserializeObject(content));
        }

        // 6. GET /api/page/post/{postId}/likes - Lấy danh sách lượt thích
        [HttpGet("page/post/{postId}/likes")]
        public async Task<IActionResult> GetLikes(string postId)
        {
            var client = _httpClientFactory.CreateClient();
            // Thêm summary=true để lấy tổng số lượng like
            var response = await client.GetAsync($"{_fbBaseUrl}/{postId}/likes?summary=true&access_token={_accessToken}");

            var content = await response.Content.ReadAsStringAsync();
            return Ok(JsonConvert.DeserializeObject(content));
        }

        // 7. GET /api/page/{pageId}/insights - Lấy số liệu thống kê
        [HttpGet("page/{pageId}/insights")]
        public async Task<IActionResult> GetInsights(string pageId)
        {
            var client = _httpClientFactory.CreateClient();
            // Các metric phổ biến: page_impressions (lượt hiển thị), page_engaged_users (người dùng tương tác)
            var metrics = "page_impressions,page_engaged_users";
            var response = await client.GetAsync($"{_fbBaseUrl}/{pageId}/insights?metric={metrics}&access_token={_accessToken}");

            var content = await response.Content.ReadAsStringAsync();
            return Ok(JsonConvert.DeserializeObject(content));
        }
    }

    // Class bổ trợ để nhận nội dung message từ JSON body
    public class PostRequest
    {
        public string Message { get; set; }
    }
}
