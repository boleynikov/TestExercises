--3.1
Select NTOV, 
       Sum(Kol) as SumKol, 
	   Sum(Kol*Cena) as SumMoney 
from DMS
	join TOV on TOV.KTOV = DMS.KTOV
	join DMZ on DMZ.NDM = DMS.NDM
where DMZ.PR = 2 and DMZ.DDM = '2020-01-04'
group by NTOV
order by SumMoney desc

--3.2
Update DMS
	Set DMS.SORT = TOV.SORT 
from DMS 
	join TOV on DMS.KTOV = TOV.KTOV


--3.3
Select NTOV, 
       SUM(KOL*(3-PR)-KOL*PR) as Rem, 
	   SUM((KOL*(3-PR)-KOL*PR) * DMS.CENA) as RemMoney 
from DMS
	join TOV on TOV.KTOV = DMS.KTOV
	join DMZ on DMZ.NDM = DMS.NDM
Group by NTOV
Order by NTOV


--3.4
Declare @prihod INT,
		@rashod INT,
		@newPR INT
Set @prihod = (Select Count(*) from DMZ Where PR = 1);
Set @rashod = (Select Count(*) from DMZ Where PR = 2);
IF @prihod > @rashod
	Set @newPR = 2
ELSE
	Set @newPR = 1
Insert into DMZ 
Select MAX(NDM) + 1, 
       GETDATE(), 
	   @newPR
from DMZ
