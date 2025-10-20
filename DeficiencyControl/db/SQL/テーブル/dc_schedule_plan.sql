-- Table: dc_schedule_plan

-- DROP TABLE dc_schedule_plan;

CREATE TABLE dc_schedule_plan
(

  ms_vessel_id numeric(4, 0),
  
  CONSTRAINT pk_dc_schedule_plan PRIMARY KEY (schedule_id )
)
INHERITS (dc_schedule)
WITH (
  OIDS=FALSE
);
ALTER TABLE dc_schedule_plan
  OWNER TO "IGTDefconUser";
COMMENT ON TABLE dc_schedule_plan
  IS 'スケジュール予定実績テーブル';

-- Trigger: tr_dc_schedule_plan on dc_schedule_plan

-- DROP TRIGGER tr_dc_schedule_plan ON dc_schedule_plan;

CREATE TRIGGER tr_dc_schedule_plan
  BEFORE INSERT OR UPDATE
  ON dc_schedule_plan
  FOR EACH ROW
  EXECUTE PROCEDURE "TrAutoDateUpdate"();

