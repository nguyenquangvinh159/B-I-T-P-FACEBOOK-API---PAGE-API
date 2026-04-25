using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebhookController : ControllerBase
    {
        // Đây là mã bí mật dùng để xác thực giữa Facebook và Server của bạn
        // Bạn sẽ nhập chuỗi này vào ô "Verify Token" trên Facebook Developer Dashboard
        private readonly string myVerifyToken = "VinhQuang_Verify_Token_2024";

        /// <summary>
        /// Endpoint để Facebook xác thực Webhook (Phương thức GET)
        /// </summary>
        [HttpGet]
        public IActionResult VerifyWebhook(
            [FromQuery(Name = "hub.mode")] string mode,
            [FromQuery(Name = "hub.verify_token")] string token,
            [FromQuery(Name = "hub.challenge")] string challenge)
        {
            // 1. Kiểm tra xem Facebook có gửi đúng chế độ "subscribe" và mã token không
            if (mode == "subscribe" && token == myVerifyToken)
            {
                // 2. Nếu đúng, trả về mã challenge (định dạng text/plain) để xác nhận
                Console.WriteLine("Webhook Verified Successfully!");
                return Ok(challenge);
            }

            // 3. Nếu sai mã, trả về lỗi 403 Forbidden
            return Forbid();
        }

        /// <summary>
        /// Endpoint để nhận dữ liệu thực tế từ Facebook (Phương thức POST)
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> ReceiveNotification([FromBody] object data) // Đổi từ dynamic sang object để an toàn
        {
            // LOG DÒNG NÀY ĐẦU TIÊN
            Console.WriteLine(">>>>>> ĐÃ NHẬN YÊU CẦU POST TỪ FACEBOOK <<<<<<");

            if (data != null)
            {
                Console.WriteLine("Dữ liệu: " + data.ToString());
            }

            /* TẠM THỜI COMMENT (KHÓA) TOÀN BỘ ĐOẠN CODE KAFKA DƯỚI ĐÂY
            var config = new ProducerConfig { ... };
            ...
            */

            return Ok();
        }
    }
}
