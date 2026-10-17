-- =========================================================================
-- Buoi 5 - Tiet 3: Stored Procedure bao cao doanh thu
-- Chay tren SSMS, chon dung database cua project (appsettings.json: DBTEST)
-- Luu y: bang Orders cua project dung OrderDate / TotalAmount (thay cho CreatedDate / FinalAmount)
--        va khong luu RewardPoints -> diem thuong tinh lai theo quy tac 10.000d = 1 diem
-- =========================================================================
USE DBTEST;
GO

-- =========================================================================
-- 1. sp_GetRevenueByCashier: Tong hop doanh thu theo thu ngan
-- =========================================================================
IF OBJECT_ID('sp_GetRevenueByCashier', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetRevenueByCashier;
GO

CREATE PROCEDURE sp_GetRevenueByCashier
AS
BEGIN
    -- SET NOCOUNT ON: Ngan SQL Server gui thong bao so dong bi anh huong (DoneInProc)
    SET NOCOUNT ON;

    SELECT
        CashierUsername,
        COUNT(OrderId) AS TotalOrders,
        SUM(TotalAmount) AS TotalRevenue,
        SUM(CAST(FLOOR(TotalAmount / 10000) AS INT)) AS TotalPointsAwarded
    FROM Orders
    GROUP BY CashierUsername
    ORDER BY TotalRevenue DESC; -- Sap xep doanh thu tu cao xuong thap
END
GO

-- =========================================================================
-- 2. Bai tap 3.1 - sp_GetRevenueByDateRange: Doanh thu theo khoang ngay (co tham so)
-- =========================================================================
IF OBJECT_ID('sp_GetRevenueByDateRange', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetRevenueByDateRange;
GO

CREATE PROCEDURE sp_GetRevenueByDateRange
    @FromDate DATETIME,
    @ToDate DATETIME
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        CashierUsername,
        COUNT(OrderId) AS TotalOrders,
        SUM(TotalAmount) AS TotalRevenue,
        SUM(CAST(FLOOR(TotalAmount / 10000) AS INT)) AS TotalPointsAwarded
    FROM Orders
    WHERE OrderDate >= @FromDate AND OrderDate <= @ToDate
    GROUP BY CashierUsername
    ORDER BY TotalRevenue DESC;
END
GO

-- =========================================================================
-- 3. Bai tap 3.2 - sp_GetTop5SellingProducts: Top 5 san pham ban chay nhat
-- =========================================================================
IF OBJECT_ID('sp_GetTop5SellingProducts', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetTop5SellingProducts;
GO

CREATE PROCEDURE sp_GetTop5SellingProducts
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 5
        p.ProductId,
        p.ProductName,
        SUM(od.Quantity) AS TotalQuantitySold,
        SUM(od.Quantity * od.UnitPrice) AS TotalRevenue
    FROM OrderDetails od
    INNER JOIN Products p ON p.ProductId = od.ProductId
    GROUP BY p.ProductId, p.ProductName
    ORDER BY TotalQuantitySold DESC;
END
GO

-- Kiem tra nhanh
EXEC sp_GetRevenueByCashier;
EXEC sp_GetRevenueByDateRange @FromDate = '2026-01-01', @ToDate = '2026-12-31';
EXEC sp_GetTop5SellingProducts;
