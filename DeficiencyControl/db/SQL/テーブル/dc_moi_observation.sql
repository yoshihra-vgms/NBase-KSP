-- Table: dc_moi_observation

-- DROP TABLE dc_moi_observation;

CREATE TABLE dc_moi_observation
(
  moi_observation_id serial NOT NULL,

  moi_id integer,
  observation_no integer,
  moi_status_id integer,
  viq_code_id integer,
  viq_no_id integer,
  observation text,
  
  root_cause character varying,
  comment_1st text,
  comment_1st_check boolean,
  comment_2nd text,
  comment_2nd_check boolean,
  preventive_action text,
  special_notes text,
  search_keyword text,
  
  
  delete_flag boolean DEFAULT false,
  create_ms_user_id character varying(40),
  update_ms_user_id character varying(40),
  create_date timestamp without time zone,
  update_date timestamp without time zone,
  CONSTRAINT pk_dc_moi_observation PRIMARY KEY (moi_observation_id )
)
WITH (
  OIDS=FALSE
);
ALTER TABLE dc_moi_observation
  OWNER TO "IGTDefconUser";
COMMENT ON TABLE dc_moi_observation
  IS '検船指摘事項テーブル';

-- Trigger: tr_dc_moi_observation on dc_moi_observation

-- DROP TRIGGER tr_dc_moi_observation ON dc_moi_observation;

CREATE TRIGGER tr_dc_moi_observation
  BEFORE INSERT OR UPDATE
  ON dc_moi_observation
  FOR EACH ROW
  EXECUTE PROCEDURE "TrAutoDateUpdate"();

