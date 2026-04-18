# Facebook Graph API Integration with ASP.NET Core

Dự án này là một ASP.NET Core Web API được thiết kế để tương tác với Facebook Graph API nhằm quản lý các Fanpage. Dự án cho phép thực hiện các thao tác cơ bản như lấy thông tin trang, quản lý bài viết, xem tương tác và phân tích số liệu.

## Các tính năng chính (API Endpoints)

Hệ thống cung cấp các endpoint sau để thao tác với Facebook Page qua `pageId` và `postId`:

### 1. Quản lý Trang (Page)
* **GET** `/api/facebook/page/{pageId}`: Lấy thông tin cơ bản của Page (Tên, giới thiệu, lượt like).
* **GET** `/api/facebook/page/{pageId}/insights`: Truy xuất các số liệu thống kê như lượt hiển thị (impressions) và người dùng tương tác (engaged users).

### 2. Quản lý Bài viết (Posts)
* **GET** `/api/facebook/page/{pageId}/posts`: Lấy danh sách tất cả các bài viết đã đăng trên Page.
* **POST** `/api/facebook/page/{pageId}/posts`: Đăng bài viết mới lên tường của Page.
* **DELETE** `/api/facebook/page/post/{postId}`: Xóa một bài viết cụ thể dựa trên ID.

### 3. Quản lý Tương tác
* **GET** `/api/facebook/page/post/{postId}/comments`: Lấy danh sách các bình luận trong một bài viết.
* **GET** `/api/facebook/page/post/{postId}/likes`: Xem danh sách các lượt thích và tổng số lượng tương tác của bài viết.

## Cấu hình hệ thống

### Yêu cầu tiên quyết
1.  **Meta for Developers App**: Một ứng dụng loại **Business** được tạo trên trang Facebook Developers.
2.  **Page Access Token**: Mã truy cập có các quyền sau:
    * `pages_manage_posts`
    * `pages_read_engagement`
    * `pages_show_list`
    * `read_insights`
3.  **Page ID**: ID của Fanpage mục tiêu (Ví dụ: `61569227309171`).

### Cấu hình Project
Để chạy ứng dụng, hãy cập nhật `Page Access Token` vào biến môi trường hoặc cấu hình trong `FacebookController.cs`:
