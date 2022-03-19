-- [196. Delete Duplicate Emails](https://leetcode.com/problems/delete-duplicate-emails/) 删除重复的电子邮箱
-- 基本思路是找出重复的email的id，删掉。

DELETE
FROM Person
WHERE id NOT IN (
    SELECT MIN(id)
    FROM Person
    GROUP BY email
)

-- MySQL不允许直接删除子查询的表，要再SELECT一遍放到FROM里
DELETE
FROM Person
WHERE id NOT IN (SELECT * FROM (
    SELECT MIN(id) FROM Person
    GROUP BY email
) t);

-- 标准答案，自连接
DELETE p1
FROM Person p1, Person p2
WHERE p1.email = p2.email AND p1.id > p2.id

-- TODO: 窗口函数效率很高
