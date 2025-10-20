-- Table: ms_vessel_schedule_kind_detail_enable

-- DROP TABLE ms_vessel_schedule_kind_detail_enable;

CREATE TABLE ms_vessel_schedule_kind_detail_enable
(

  ms_vessel_id numeric(4, 0) NOT NULL,
  schedule_kind_detail_id integer NOT NULL,

  enabled boolean,



  delete_flag boolean DEFAULT false,
  create_ms_user_id character varying(40),
  update_ms_user_id character varying(40),
  create_date timestamp without time zone,
  update_date timestamp without time zone,
  CONSTRAINT pk_ms_vessel_schedule_kind_detail_enable PRIMARY KEY (ms_vessel_id, schedule_kind_detail_id )
)
WITH (
  OIDS=FALSE
);
ALTER TABLE ms_vessel_schedule_kind_detail_enable
  OWNER TO "IGTDefconUser";
COMMENT ON TABLE ms_vessel_schedule_kind_detail_enable
  IS 'スケジュール種別詳細有効マスタ';

-- Trigger: tr_ms_vessel_schedule_kind_detail_enable on ms_vessel_schedule_kind_detail_enable

-- DROP TRIGGER tr_ms_vessel_schedule_kind_detail_enable ON ms_vessel_schedule_kind_detail_enable;

CREATE TRIGGER tr_ms_vessel_schedule_kind_detail_enable
  BEFORE INSERT OR UPDATE
  ON ms_vessel_schedule_kind_detail_enable
  FOR EACH ROW
  EXECUTE PROCEDURE "TrAutoDateUpdate"();