-- Table: dc_accident

-- DROP TABLE dc_accident;

CREATE TABLE dc_accident
(
  accident_id serial NOT NULL,
  
  date timestamp without time zone,
  ms_user_id character varying(40),
  accident_kind_id integer,
  kind_of_accident_id integer,
  accident_situation_id integer,
  ms_basho_id character varying(40),
  ms_vessel_id numeric(4, 0),
  ms_regional_code character varying(10),
  title character varying,
  accident_report_no character varying,
  accident_importance_id integer,
  accident_status_id integer,
  
  accident text,
  spot_report text,
  cause_of_accident text,
  preventive_action text,
  influence text,
  remarks text,  
  search_keyword text,
  
  delete_flag boolean DEFAULT false,
  create_ms_user_id character varying(40),
  update_ms_user_id character varying(40),
  create_date timestamp without time zone,
  update_date timestamp without time zone,
  CONSTRAINT pk_dc_accident PRIMARY KEY (accident_id )
)
WITH (
  OIDS=FALSE
);
ALTER TABLE dc_accident
  OWNER TO "IGTDefconUser";
COMMENT ON TABLE dc_accident
  IS '事故トラブルテーブル';

-- Trigger: tr_dc_accident on dc_accident

-- DROP TRIGGER tr_dc_accident ON dc_accident;

CREATE TRIGGER tr_dc_accident
  BEFORE INSERT OR UPDATE
  ON dc_accident
  FOR EACH ROW
  EXECUTE PROCEDURE "TrAutoDateUpdate"();

