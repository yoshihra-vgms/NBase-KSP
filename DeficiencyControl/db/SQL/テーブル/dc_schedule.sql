-- Table: dc_schedule

-- DROP TABLE dc_schedule;

CREATE TABLE dc_schedule
(
  schedule_id serial NOT NULL,
  
  schedule_category_id integer,
  schedule_kind_id integer,
  schedule_kind_detail_id integer,
  
  estimate_date timestamp without time zone,
  inspection_date timestamp without time zone,
  expiry_date timestamp without time zone,
  
  record_memo character varying,
  
  color_r integer,
  color_g integer,
  color_b integer,

  
  
  delete_flag boolean DEFAULT false,
  create_ms_user_id character varying(40),
  update_ms_user_id character varying(40),
  create_date timestamp without time zone,
  update_date timestamp without time zone,
  CONSTRAINT pk_dc_schedule PRIMARY KEY (schedule_id )
)
WITH (
  OIDS=FALSE
);
ALTER TABLE dc_schedule
  OWNER TO "IGTDefconUser";
COMMENT ON TABLE dc_schedule
  IS 'スケジュールテーブル';

-- Trigger: tr_dc_schedule on dc_schedule

-- DROP TRIGGER tr_dc_schedule ON dc_schedule;

CREATE TRIGGER tr_dc_schedule
  BEFORE INSERT OR UPDATE
  ON dc_schedule
  FOR EACH ROW
  EXECUTE PROCEDURE "TrAutoDateUpdate"();

