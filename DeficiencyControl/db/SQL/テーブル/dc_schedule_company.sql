-- Table: dc_schedule_company

-- DROP TABLE dc_schedule_company;

CREATE TABLE dc_schedule_company
(

  
  CONSTRAINT pk_dc_schedule_company PRIMARY KEY (schedule_id )
)
INHERITS (dc_schedule)
WITH (
  OIDS=FALSE
);
ALTER TABLE dc_schedule_company
  OWNER TO "IGTDefconUser";
COMMENT ON TABLE dc_schedule_company
  IS 'スケジュール会社テーブル';

-- Trigger: tr_dc_schedule_company on dc_schedule_company

-- DROP TRIGGER tr_dc_schedule_company ON dc_schedule_company;

CREATE TRIGGER tr_dc_schedule_company
  BEFORE INSERT OR UPDATE
  ON dc_schedule_company
  FOR EACH ROW
  EXECUTE PROCEDURE "TrAutoDateUpdate"();

