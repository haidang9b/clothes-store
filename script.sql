CREATE DATABASE ClothingStore
GO
USE [ClothingStore]
GO
/****** Object:  Table [dbo].[Bill]    Script Date: 4/23/2022 4:55:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bill](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[status] [int] NOT NULL,
	[address] [nvarchar](max) NULL,
	[numberPhone] [nvarchar](max) NULL,
	[createdDate] [datetimeoffset](7) NOT NULL,
	[nameReceiver] [nvarchar](max) NULL,
	[updateDate] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_Bill] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BillDetail]    Script Date: 4/23/2022 4:55:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillDetail](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[bill_id] [int] NOT NULL,
	[product_id] [int] NOT NULL,
	[quantity] [int] NOT NULL,
 CONSTRAINT [PK_BillDetail] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 4/23/2022 4:55:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 4/23/2022 4:55:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](max) NULL,
	[address] [nvarchar](max) NULL,
	[phoneNumber] [nvarchar](max) NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 4/23/2022 4:55:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](max) NULL,
	[price] [float] NOT NULL,
	[quantity] [int] NOT NULL,
	[description] [nvarchar](max) NULL,
	[image] [nvarchar](max) NULL,
	[category_id] [int] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 4/23/2022 4:55:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Store]    Script Date: 4/23/2022 4:55:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Store](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](max) NULL,
	[address] [nvarchar](max) NULL,
	[phoneNumber] [nvarchar](max) NULL,
 CONSTRAINT [PK_Store] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 4/23/2022 4:55:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[FullName] [nvarchar](max) NULL,
	[role_id] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Bill] ON 

INSERT [dbo].[Bill] ([id], [user_id], [status], [address], [numberPhone], [createdDate], [nameReceiver], [updateDate]) VALUES (1, 1, 3, N'dasdasdsada', N'123456', CAST(N'0001-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, CAST(N'2022-04-18T14:56:11.8251542+07:00' AS DateTimeOffset))
INSERT [dbo].[Bill] ([id], [user_id], [status], [address], [numberPhone], [createdDate], [nameReceiver], [updateDate]) VALUES (2, 1, 3, N'dasdasdsadassssssss', N'123456', CAST(N'0001-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, CAST(N'2022-04-18T14:56:58.9484708+07:00' AS DateTimeOffset))
INSERT [dbo].[Bill] ([id], [user_id], [status], [address], [numberPhone], [createdDate], [nameReceiver], [updateDate]) VALUES (3, 3, 1, N'Ho Chi Minh - 2ww - ewew - 19 Nguyen Huu Tho', N'326889240', CAST(N'2022-04-18T19:58:21.7500129+07:00' AS DateTimeOffset), N'Dang Phan Hai', CAST(N'2022-04-18T19:58:21.7501079+07:00' AS DateTimeOffset))
INSERT [dbo].[Bill] ([id], [user_id], [status], [address], [numberPhone], [createdDate], [nameReceiver], [updateDate]) VALUES (4, 3, 3, N'Ho Chi Minh - 2ww - ewew - 19 Nguyen Huu Tho', N'326889240', CAST(N'2022-04-18T19:58:24.8264645+07:00' AS DateTimeOffset), N'Dang Phan Hai', CAST(N'2022-04-18T19:58:58.2057436+07:00' AS DateTimeOffset))
INSERT [dbo].[Bill] ([id], [user_id], [status], [address], [numberPhone], [createdDate], [nameReceiver], [updateDate]) VALUES (5, 3, 1, N'Ho Chi Minh - asd - dsadsad - 19 Nguyen Huu Tho', N'326889240', CAST(N'2022-04-18T20:10:23.1831907+07:00' AS DateTimeOffset), N'Dang Phan Hai', CAST(N'2022-04-18T20:10:23.1831953+07:00' AS DateTimeOffset))
INSERT [dbo].[Bill] ([id], [user_id], [status], [address], [numberPhone], [createdDate], [nameReceiver], [updateDate]) VALUES (6, 3, 3, N'Ho Chi Minh - asd - dsadsad - 19 Nguyen Huu Tho', N'326889240', CAST(N'2022-04-18T20:10:26.2435776+07:00' AS DateTimeOffset), N'Dang Phan Hai', CAST(N'2022-04-22T00:46:21.4151459+07:00' AS DateTimeOffset))
INSERT [dbo].[Bill] ([id], [user_id], [status], [address], [numberPhone], [createdDate], [nameReceiver], [updateDate]) VALUES (7, 3, 1, N'Ho Chi Minh - asd - asd - 19 Nguyen Huu Tho', N'326889240', CAST(N'2022-04-18T20:10:42.4815023+07:00' AS DateTimeOffset), N'Dang Phan Hai', CAST(N'2022-04-18T20:10:42.4815052+07:00' AS DateTimeOffset))
INSERT [dbo].[Bill] ([id], [user_id], [status], [address], [numberPhone], [createdDate], [nameReceiver], [updateDate]) VALUES (8, 3, 1, N'Ho Chi Minh - qư -  - 19 Nguyen Huu Tho', N'326889240', CAST(N'2022-04-18T20:16:37.8999893+07:00' AS DateTimeOffset), N'Dang Phan Hai', CAST(N'2022-04-18T20:16:37.9000868+07:00' AS DateTimeOffset))
INSERT [dbo].[Bill] ([id], [user_id], [status], [address], [numberPhone], [createdDate], [nameReceiver], [updateDate]) VALUES (9, 3, 3, N'Ho Chi Minh - Quận 7 - tân Hưng - 19 Nguyen Huu Tho', N'326889240', CAST(N'2022-04-18T20:19:36.6089247+07:00' AS DateTimeOffset), N'Dang Phan Hai', CAST(N'2022-04-18T20:19:58.9877603+07:00' AS DateTimeOffset))
INSERT [dbo].[Bill] ([id], [user_id], [status], [address], [numberPhone], [createdDate], [nameReceiver], [updateDate]) VALUES (10, 3, 1, N'Ho Chi Minh - asđ - ddd - 19 Nguyen Huu Tho', N'326889240', CAST(N'2022-04-18T20:40:27.0662116+07:00' AS DateTimeOffset), N'Dang Phan Hai', CAST(N'2022-04-18T20:40:27.0662166+07:00' AS DateTimeOffset))
INSERT [dbo].[Bill] ([id], [user_id], [status], [address], [numberPhone], [createdDate], [nameReceiver], [updateDate]) VALUES (11, 10, 4, N'Ho Chi Minh - Quận 7 - Tân phong - 19 Nguyen Huu Tho', N'326889240', CAST(N'2022-04-18T21:45:22.6122091+07:00' AS DateTimeOffset), N'Dang Phan Hai', CAST(N'2022-04-22T00:46:11.7141249+07:00' AS DateTimeOffset))
INSERT [dbo].[Bill] ([id], [user_id], [status], [address], [numberPhone], [createdDate], [nameReceiver], [updateDate]) VALUES (12, 8, 3, N'Ho Chi Minh - asd - ddd - 19 Nguyen Huu Tho', N'123654789', CAST(N'2022-04-21T19:20:33.8988858+07:00' AS DateTimeOffset), N'Le Van Quoc Thang', CAST(N'2022-04-22T00:25:14.3974336+07:00' AS DateTimeOffset))
SET IDENTITY_INSERT [dbo].[Bill] OFF
GO
SET IDENTITY_INSERT [dbo].[BillDetail] ON 

INSERT [dbo].[BillDetail] ([id], [bill_id], [product_id], [quantity]) VALUES (1, 1, 14, 1)
INSERT [dbo].[BillDetail] ([id], [bill_id], [product_id], [quantity]) VALUES (2, 1, 15, 1)
INSERT [dbo].[BillDetail] ([id], [bill_id], [product_id], [quantity]) VALUES (3, 2, 14, 1)
INSERT [dbo].[BillDetail] ([id], [bill_id], [product_id], [quantity]) VALUES (4, 2, 15, 1)
INSERT [dbo].[BillDetail] ([id], [bill_id], [product_id], [quantity]) VALUES (5, 3, 19, 1)
INSERT [dbo].[BillDetail] ([id], [bill_id], [product_id], [quantity]) VALUES (6, 3, 19, 1)
INSERT [dbo].[BillDetail] ([id], [bill_id], [product_id], [quantity]) VALUES (7, 3, 19, 1)
INSERT [dbo].[BillDetail] ([id], [bill_id], [product_id], [quantity]) VALUES (8, 4, 19, 1)
INSERT [dbo].[BillDetail] ([id], [bill_id], [product_id], [quantity]) VALUES (9, 4, 19, 1)
INSERT [dbo].[BillDetail] ([id], [bill_id], [product_id], [quantity]) VALUES (10, 4, 19, 1)
INSERT [dbo].[BillDetail] ([id], [bill_id], [product_id], [quantity]) VALUES (11, 5, 19, 1)
INSERT [dbo].[BillDetail] ([id], [bill_id], [product_id], [quantity]) VALUES (12, 5, 19, 1)
INSERT [dbo].[BillDetail] ([id], [bill_id], [product_id], [quantity]) VALUES (13, 5, 19, 1)
INSERT [dbo].[BillDetail] ([id], [bill_id], [product_id], [quantity]) VALUES (14, 6, 19, 1)
INSERT [dbo].[BillDetail] ([id], [bill_id], [product_id], [quantity]) VALUES (15, 6, 19, 1)
INSERT [dbo].[BillDetail] ([id], [bill_id], [product_id], [quantity]) VALUES (16, 6, 19, 1)
INSERT [dbo].[BillDetail] ([id], [bill_id], [product_id], [quantity]) VALUES (17, 7, 19, 1)
INSERT [dbo].[BillDetail] ([id], [bill_id], [product_id], [quantity]) VALUES (18, 7, 19, 1)
INSERT [dbo].[BillDetail] ([id], [bill_id], [product_id], [quantity]) VALUES (19, 7, 19, 1)
INSERT [dbo].[BillDetail] ([id], [bill_id], [product_id], [quantity]) VALUES (20, 8, 19, 1)
INSERT [dbo].[BillDetail] ([id], [bill_id], [product_id], [quantity]) VALUES (21, 8, 19, 1)
INSERT [dbo].[BillDetail] ([id], [bill_id], [product_id], [quantity]) VALUES (22, 9, 20, 1)
INSERT [dbo].[BillDetail] ([id], [bill_id], [product_id], [quantity]) VALUES (23, 9, 20, 1)
INSERT [dbo].[BillDetail] ([id], [bill_id], [product_id], [quantity]) VALUES (24, 9, 20, 1)
INSERT [dbo].[BillDetail] ([id], [bill_id], [product_id], [quantity]) VALUES (25, 10, 18, 1)
INSERT [dbo].[BillDetail] ([id], [bill_id], [product_id], [quantity]) VALUES (26, 10, 18, 1)
INSERT [dbo].[BillDetail] ([id], [bill_id], [product_id], [quantity]) VALUES (27, 10, 19, 1)
INSERT [dbo].[BillDetail] ([id], [bill_id], [product_id], [quantity]) VALUES (28, 10, 19, 1)
INSERT [dbo].[BillDetail] ([id], [bill_id], [product_id], [quantity]) VALUES (29, 10, 20, 1)
INSERT [dbo].[BillDetail] ([id], [bill_id], [product_id], [quantity]) VALUES (30, 10, 20, 1)
INSERT [dbo].[BillDetail] ([id], [bill_id], [product_id], [quantity]) VALUES (31, 10, 20, 1)
INSERT [dbo].[BillDetail] ([id], [bill_id], [product_id], [quantity]) VALUES (32, 11, 20, 1)
INSERT [dbo].[BillDetail] ([id], [bill_id], [product_id], [quantity]) VALUES (33, 11, 16, 1)
INSERT [dbo].[BillDetail] ([id], [bill_id], [product_id], [quantity]) VALUES (34, 11, 14, 1)
INSERT [dbo].[BillDetail] ([id], [bill_id], [product_id], [quantity]) VALUES (35, 12, 20, 1)
INSERT [dbo].[BillDetail] ([id], [bill_id], [product_id], [quantity]) VALUES (36, 12, 19, 1)
INSERT [dbo].[BillDetail] ([id], [bill_id], [product_id], [quantity]) VALUES (37, 12, 19, 1)
INSERT [dbo].[BillDetail] ([id], [bill_id], [product_id], [quantity]) VALUES (38, 12, 18, 1)
INSERT [dbo].[BillDetail] ([id], [bill_id], [product_id], [quantity]) VALUES (39, 12, 18, 1)
SET IDENTITY_INSERT [dbo].[BillDetail] OFF
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([id], [name]) VALUES (1, N'Áo')
INSERT [dbo].[Category] ([id], [name]) VALUES (2, N'Quần')
INSERT [dbo].[Category] ([id], [name]) VALUES (3, N'Underware')
INSERT [dbo].[Category] ([id], [name]) VALUES (4, N'Balo - túi xách')
INSERT [dbo].[Category] ([id], [name]) VALUES (5, N'Nón')
INSERT [dbo].[Category] ([id], [name]) VALUES (6, N'Vớ')
INSERT [dbo].[Category] ([id], [name]) VALUES (7, N'Thắt lưng')
INSERT [dbo].[Category] ([id], [name]) VALUES (8, N'Mắt kính')
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([id], [title], [price], [quantity], [description], [image], [category_id]) VALUES (13, N'FAPAS WASH EAGLE TEE2', 300, 6, N'Đang cập nhật
', N'https://localhost:44314\Uploads\Images\637858189996024088.jpg', 1)
INSERT [dbo].[Product] ([id], [title], [price], [quantity], [description], [image], [category_id]) VALUES (14, N'FAPAS CHECKED WHITE NECK TEE', 300, 2, N'Đang cập nhật
', N'https://localhost:44314\Uploads\Images\637857211104435339.jpg', 1)
INSERT [dbo].[Product] ([id], [title], [price], [quantity], [description], [image], [category_id]) VALUES (15, N'FAPAS RED EMBROIDER POLO', 350, 8, N'FAPAS RED EMBROIDER POLO
', N'https://localhost:44314\Uploads\Images\637857211427529226.jpg', 1)
INSERT [dbo].[Product] ([id], [title], [price], [quantity], [description], [image], [category_id]) VALUES (16, N'Áo Sơmi Tay Ngắn ICON DENIM Typo Pattern', 285, 9, N'ICON DENIM Slim Kaki//

Về được ít Kaki Slimfit cho ae dịp tết này. Form dáng ôm tôn “chuẩn-đét” từ ICON DENIM, chất vải kaki dày dặn chống nhão cùng gam màu “thời thượng”, phối thêm áo sơ mi hay áo thun vào là “không còn gì để lói” luôn ae ạ 👍

QKID0003', N'https://localhost:44314\Uploads\Images\637858025602924548.webp', 1)
INSERT [dbo].[Product] ([id], [title], [price], [quantity], [description], [image], [category_id]) VALUES (17, N'FAPAS WASH RIP KNEE JEANS', 500, 15, N'Quần Jeans Wash ICON DENIM Light Blue
', N'https://localhost:44314\Uploads\Images\637857212419500735.jpg', 2)
INSERT [dbo].[Product] ([id], [title], [price], [quantity], [description], [image], [category_id]) VALUES (18, N'Quần Jeans Wash ICON DENIM Light Blue', 500, 46, N'Quần Jeans Wash ICON DENIM Light Blue
', N'https://localhost:44314\Uploads\Images\637857213769275835.jpg', 2)
INSERT [dbo].[Product] ([id], [title], [price], [quantity], [description], [image], [category_id]) VALUES (19, N'Quần Short Nỉ ICON DENIM Fabric Slim', 280, 78, N'Khởi động mùa hè năng động bằng thiết kế vừa ra mắt từ ICONDENIM. Một trong những gam màu thuộc tuýp Casual dễ dàng thích nghi: Đen, Xám, Kem, cùng chất liệu kaki dày dặn vừa phải, hứa hẹn sẽ đồng hành cùng ae cho ra những outfit mát mẻ, thoải mái mùa này.

QSID0030//ICONDENIM Embroidery Logo Short Kaki

', N'https://localhost:44314\Uploads\Images\637858015814492379.webp', 2)
INSERT [dbo].[Product] ([id], [title], [price], [quantity], [description], [image], [category_id]) VALUES (20, N'Quần Short Kaki ICON DENIM Embroidery Logo', 288, 9, N'Thông tin sản phẩm
Khởi động mùa hè năng động bằng thiết kế vừa ra mắt từ ICONDENIM. Một trong những gam màu thuộc tuýp Casual dễ dàng thích nghi: Đen, Xám, Kem, cùng chất liệu kaki dày dặn vừa phải, hứa hẹn sẽ đồng hành cùng ae cho ra những outfit mát mẻ, thoải mái mùa này.

QSID0030//ICONDENIM Embroidery Logo Short Kaki', N'https://localhost:44314\Uploads\Images\637861857939300263.webp', 2)
INSERT [dbo].[Product] ([id], [title], [price], [quantity], [description], [image], [category_id]) VALUES (21, N'Quần Kaki ICON DENIM Slim', 400, 22224, N'ICON DENIM Slim Kaki//

Về được ít Kaki Slimfit cho ae dịp tết này. Form dáng ôm tôn “chuẩn-đét” từ ICON DENIM, chất vải kaki dày dặn chống nhão cùng gam màu “thời thượng”, phối thêm áo sơ mi hay áo thun vào là “không còn gì để lói” luôn ae ạ 👍

QKID0003', N'https://localhost:44314\Uploads\Images\637861857704846472.jpg', 2)
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([id], [name]) VALUES (1, N'Admin')
INSERT [dbo].[Role] ([id], [name]) VALUES (2, N'Seller')
INSERT [dbo].[Role] ([id], [name]) VALUES (3, N'Customer')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [Username], [Password], [FullName], [role_id]) VALUES (1, N'admin', N'$2y$10$6D2NliphLElqK7gmR2A.KeO0cPgbL3D5zOWKtsjccbqEUqRmc/XU6', N'Phan Hai Dang', 1)
INSERT [dbo].[User] ([Id], [Username], [Password], [FullName], [role_id]) VALUES (2, N'user', N'$2y$10$9sNM3ICZI7cmOJKiIds8rOVzZzf38Vj2ZGMm8neh1J9LfV0TZpX1y', N'User Hai Yen', 1)
INSERT [dbo].[User] ([Id], [Username], [Password], [FullName], [role_id]) VALUES (3, N'user2', N'$2y$10$BQrnN62TO.h3KBwL4V/tSe.iGLBn4O7QfiAPdal/1iBDl7YoZ2CRy', N'User Second', 2)
INSERT [dbo].[User] ([Id], [Username], [Password], [FullName], [role_id]) VALUES (4, N'adminb', N'$2y$10$r2G5NEaxEkC1xfT90NyErupLeH.M1DzlPwR35p01kOI/5/mPoIP2y', N'12311321', 3)
INSERT [dbo].[User] ([Id], [Username], [Password], [FullName], [role_id]) VALUES (5, N'mrdrom', N'$2y$10$HJI97eAk6yyXx7ojbJEAwOLkvKYbTUkAy0ZdPHEQ6vaFgX8IwaDOW', N'Yến', 3)
INSERT [dbo].[User] ([Id], [Username], [Password], [FullName], [role_id]) VALUES (6, N'tonny', N'$2y$10$HJI97eAk6yyXx7ojbJEAwOLkvKYbTUkAy0ZdPHEQ6vaFgX8IwaDOW', N'teo', 3)
INSERT [dbo].[User] ([Id], [Username], [Password], [FullName], [role_id]) VALUES (7, N'haiyen', N'$2y$10$eDXfE84djUX6DdhxbPDr8erwYFe11SI0J557CCUz6Y3MwMbCIRsni', N'Hải Yến', 3)
INSERT [dbo].[User] ([Id], [Username], [Password], [FullName], [role_id]) VALUES (8, N'miloo', N'$2y$10$29OeYRolQNMkyod0nrDCruM8cm0wLDrqf3utbAMwPN51DSnUur2My', N'Thịnh', 3)
INSERT [dbo].[User] ([Id], [Username], [Password], [FullName], [role_id]) VALUES (9, N'test', N'$2y$10$.bOd9zBFCJnL2B3wBOB6yuTAxnhzL.TNWgoGKP0RpuKAViLRGatSK', N'Yến', 3)
INSERT [dbo].[User] ([Id], [Username], [Password], [FullName], [role_id]) VALUES (10, N'angular', N'$2y$10$LJ.oqKQDXS7mY2dwCU4o0eKeu4lY/O2dinwZ7GT.EqE2Gdi51nsJu', N'Hai Yen Xinh Dep', 1)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[Bill] ADD  DEFAULT ('0001-01-01T00:00:00.0000000+00:00') FOR [createdDate]
GO
ALTER TABLE [dbo].[Bill] ADD  DEFAULT ('0001-01-01T00:00:00.0000000+00:00') FOR [updateDate]
GO
ALTER TABLE [dbo].[Bill]  WITH CHECK ADD  CONSTRAINT [FK_Bill_User_user_id] FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Bill] CHECK CONSTRAINT [FK_Bill_User_user_id]
GO
ALTER TABLE [dbo].[BillDetail]  WITH CHECK ADD  CONSTRAINT [FK_BillDetail_Bill_bill_id] FOREIGN KEY([bill_id])
REFERENCES [dbo].[Bill] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BillDetail] CHECK CONSTRAINT [FK_BillDetail_Bill_bill_id]
GO
ALTER TABLE [dbo].[BillDetail]  WITH CHECK ADD  CONSTRAINT [FK_BillDetail_Product_product_id] FOREIGN KEY([product_id])
REFERENCES [dbo].[Product] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BillDetail] CHECK CONSTRAINT [FK_BillDetail_Product_product_id]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category_category_id] FOREIGN KEY([category_id])
REFERENCES [dbo].[Category] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category_category_id]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role_role_id] FOREIGN KEY([role_id])
REFERENCES [dbo].[Role] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role_role_id]
GO
