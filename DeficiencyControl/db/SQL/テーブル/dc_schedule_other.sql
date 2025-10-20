-- Table: dc_schedule_other

-- DROP TABLE dc_schedule_other;

CREATE TABLE dc_schedule_other
(
  
  event_memo character varying,
  
  CONSTRAINT pk_dc_schedule_other PRIMARY KEY (schedule_id )
)
INHERITS (dc_schedule)
WITH (
  OIDS=FALSE
);
ALTER TABLE dc_schedule_other
  OWNER TO "IGTDefconUser";
COMMENT ON TABLE dc_schedule_other
  IS 'スケジュールその他テーブル';

-- Trigger: tr_dc_schedule_other on dc_schedule_other

-- DROP TRIGGER tr_dc_schedule_other ON dc_schedule_other;

CREATE TRIGGER tr_dc_schedule_other
  BEFORE INSERT OR UPDATE
  ON dc_schedule_other
  FOR EACH ROW
  EXECUTE PROCEDURE "TrAutoDateUpdate"();

