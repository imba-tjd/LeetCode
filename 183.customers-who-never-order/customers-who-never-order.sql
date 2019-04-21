/* Write your T-SQL query statement below */
select Name as Customers
from Customers
left join Orders on Customers.Id = Orders.CustomerId
where Orders.Id is null;

select Name as Customers
from Customers
where Id not in (
    select CustomerId
    from Orders
);
