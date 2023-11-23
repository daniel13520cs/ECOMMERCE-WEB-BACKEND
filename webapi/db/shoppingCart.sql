create database shopping_cart;
use shopping_cart;
CREATE TABLE `user` (
  `username` varchar(255) PRIMARY KEY,
  `password` text,
  `created_at` timestamp,
  `modified_at` timestamp
);

CREATE TABLE `shopping_session` (
  `username` varchar(255),
  `session_token` varchar(255),
  `created_at` timestamp,
  `modified_at` timestamp,
  PRIMARY KEY (`username`, `session_token`),
  INDEX (`session_token`)
);

CREATE TABLE `cart_item` (
  `session_token` varchar(255),
  `product_id` integer,
  `quantity` integer,
  `created_at` timestamp,
  `modified_at` timestamp,
  PRIMARY KEY (`session_token`, `product_id`)
);

CREATE TABLE `product` (
  `id` integer AUTO_INCREMENT PRIMARY KEY,
  `name` varchar(255),
  `desc` text,
  `SKU` varchar(255),
  `maxQuantity` int,
  `category` varchar(255),
  `price` int,
  `imageURL` text,
  `currency` varchar(255),
  `created_at` timestamp,
  `modified_at` timestamp
);

ALTER TABLE `cart_item` ADD FOREIGN KEY (`product_id`) REFERENCES `product` (`id`);

ALTER TABLE `shopping_session` ADD FOREIGN KEY (`username`) REFERENCES `user` (`username`);

ALTER TABLE `cart_item` ADD FOREIGN KEY (`session_token`) REFERENCES `shopping_session` (`session_token`);
