-- Table: dc_moi_observation_attachment

-- DROP TABLE dc_moi_observation_attachment;

CREATE TABLE dc_moi_observation_attachment
(
  moi_observation_id integer NOT NULL,
  attachment_id integer NOT NULL,
  
  delete_flag boolean DEFAULT false,
  create_ms_user_id character varying(40),
  update_ms_user_id character varying(40),
  create_date timestamp without time zone,
  update_date timestamp without time zone,
  CONSTRAINT pk_dc_moi_observation_attachment PRIMARY KEY (moi_observation_id , attachment_id )
)
WITH (
  OIDS=FALSE
);
ALTER TABLE dc_moi_observation_attachment
  OWNER TO "IGTDefconUser";
COMMENT ON TABLE dc_moi_observation_attachment
  IS '検船指摘事項添付ファイルテーブル';

-- Trigger: tr_dc_moi_observation_attachment on dc_moi_observation_attachment

-- DROP TRIGGER tr_dc_moi_observation_attachment ON dc_moi_observation_attachment;

CREATE TRIGGER tr_dc_moi_observation_attachment
  BEFORE INSERT OR UPDATE
  ON dc_moi_observation_attachment
  FOR EACH ROW
  EXECUTE PROCEDURE "TrAutoDateUpdate"();

