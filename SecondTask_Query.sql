/* 2ое задание:
В базе данных MS SQL Server есть продукты и категории.
Одному продукту может соответствовать много категорий, в одной категории может быть много продуктов. Напишите SQL запрос для выбора всех пар «Имя продукта – Имя категории».
Если у продукта нет категорий, то его имя все равно должно выводиться.
*/

CREATE TABLE Products (
	Id INT PRIMARY KEY,
	"ProductName" TEXT
);

CREATE TABLE Categories (
	Id INT PRIMARY KEY,
	"CategoryName" TEXT
);

CREATE TABLE CategoriesProducts (
	ProductId INT FOREIGN KEY REFERENCES Products(Id),
	CategoryId INT FOREIGN KEY REFERENCES Categories(Id),
);

SELECT Prod."ProductName", Cat."CategoryName"
FROM Products Prod LEFT JOIN CategoriesProducts ProdCat ON Prod.Id = ProdCat.ProductId
LEFT JOIN Categories Cat ON ProdCat.CategoryId = Cat.Id;
