--select * from Queues;
--select top 100 * from CfdiHistories;

declare @i int = 1;
while(@i <= 60000) --60000 
begin
	--add zeros to number
	declare @n int = 5 - len(cast(@i as varchar(5)))
	declare @iString varchar(5)

	if (@n > 0)
	begin
		select @iString = replicate('0', @n) + cast(@i as varchar(5))
	end
	else
	begin
		select @iString = cast(@i as varchar(5))
	end

	--select @iString;
	select @i = @i + 1
	insert into CfdiHistories values ('Clave ' + @iString,  @iString, 'A', 'Invoice', 'ABCDEF', '220510', '221010', 'abc', '220511', '221010');
end
