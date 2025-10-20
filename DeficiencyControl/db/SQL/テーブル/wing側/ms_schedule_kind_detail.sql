-- Table: ms_schedule_kind_detail

-- DROP TABLE ms_schedule_kind_detail;

CREATE TABLE ms_schedule_kind_detail
(
  schedule_kind_detail_id serial NOT NULL,
  schedule_kind_id integer,
  schedule_kind_detail_name character varying,
  color_r integer,
  color_g integer,
  color_b integer,

  delete_flag boolean DEFAULT false,
  create_ms_user_id character varying(40),
  update_ms_user_id character varying(40),
  create_date timestamp without time zone,
  update_date timestamp without time zone,
  CONSTRAINT pk_ms_schedule_kind_detail PRIMARY KEY (schedule_kind_detail_id )
)
WITH (
  OIDS=FALSE
);
ALTER TABLE ms_schedule_kind_detail
  OWNER TO "IGTDefconUser";
COMMENT ON TABLE ms_schedule_kind_detail
  IS 'スケジュール種別詳細マスタ';

-- Trigger: tr_ms_schedule_kind_detail on ms_schedule_kind_detail

-- DROP TRIGGER tr_ms_schedule_kind_detail ON ms_schedule_kind_detail;

CREATE TRIGGER tr_ms_schedule_kind_detail
  BEFORE INSERT OR UPDATE
  ON ms_schedule_kind_detail
  FOR EACH ROW
  EXECUTE PROCEDURE "TrAutoDateUpdate"();