-- Function: "TrAutoDateUpdate"()

-- DROP FUNCTION "TrAutoDateUpdate"();

CREATE OR REPLACE FUNCTION "TrAutoDateUpdate"()
  RETURNS trigger AS
$BODY$BEGIN
	if (TG_OP = 'INSERT') then
		new."create_date" = now();
		new."update_date" = new."create_date";
	end if;

	if(TG_OP = 'UPDATE') then
		new."update_date" = now();
	end if;

	
	return new;
END;$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
ALTER FUNCTION "TrAutoDateUpdate"()
  OWNER TO "IGTDefconUser";
COMMENT ON FUNCTION "TrAutoDateUpdate"() IS '自動日付更新トリガ';
