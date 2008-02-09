SELECT * FROM vw_People WHERE EffectiveDate BETWEEN '01/01/2000' AND '12/31/2020'
ORDER BY PersonIDSystemOfRecord
--18,540 row select

SELECT * FROM vw_Purchases WHERE PurchaseDate BETWEEN '01/01/2000' AND '12/31/2020'
ORDER BY PurchaseID
--469,257 row select