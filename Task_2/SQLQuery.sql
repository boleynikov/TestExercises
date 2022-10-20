/*3.1*/
Select NTOV, 
       Sum(Kol) as SumKol, 
	   Sum(Kol*Cena) as SumMoney 
from DMS
	join TOV on TOV.KTOV = DMS.KTOV
	join DMZ on DMZ.NDM = DMS.NDM
where DMZ.PR = 2 and DMZ.DDM = '2020-01-04'
group by NTOV
order by SumMoney desc

/*3.2*/
Update DMS
	Set DMS.SORT = TOV.SORT 
from DMS 
	join TOV on DMS.KTOV = TOV.KTOV


/*3.3*/
Select NTOV, 
       SUM(KOL*(3-PR)-KOL*PR) as Rem, 
	   SUM((KOL*(3-PR)-KOL*PR) * DMS.CENA) as RemMoney 
from DMS
	join TOV on TOV.KTOV = DMS.KTOV
	join DMZ on DMZ.NDM = DMS.NDM
Group by NTOV
Order by NTOV


/*3.4*/
Declare @newNdm INT,
        @prihod INT,
		@rashod INT,
		@newPR INT
Set @prihod = (Select Count(*) from DMZ Where PR = 1);
Set @rashod = (Select Count(*) from DMZ Where PR = 2);
IF EXISTS(Select * from DMZ)
	Set @newNdm = (Select MAX(NDM) from DMZ) + 1
ELSE
	Set @newNdm = 1
IF @prihod >= @rashod
	Set @newPR = 2
ELSE
	Set @newPR = 1

Insert into DMZ 
Select @newNdm, 
	   GETDATE(), 
	   @newPR

/*3.5*/
Declare @min INT,
	    @max INT
Set @min = (Select MIN(NDM) from DMZ)
Set @max = (Select MAX(NDM) from DMZ)

Insert into DMS
Select @max, 
       KTOV, 
	   KOL, 
	   CENA, 
	   SORT 
from DMS
Where NDM = @min and 
      KTOV Not In (Select KTOV 
	               from DMS 
				   Where NDM = @max)
