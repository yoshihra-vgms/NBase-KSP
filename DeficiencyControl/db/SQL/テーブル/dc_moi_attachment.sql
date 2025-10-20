-- Table: dc_moi_attachment

-- DROP TABLE dc_moi_attachment;

CREATE TABLE dc_moi_attachment
(
  moi_id integer NOT NULL,
  attachment_id integer NOT NULL,
  
  delete_flag boolean DEFAULT false,
  create_ms_user_id character varying(40),
  update_ms_user_id character varying(40),
  create_date timestamp without time zone,
  update_date timestamp without time zone,
  CONSTRAINT pk_dc_moi_attachment PRIMARY KEY (moi_id , attachment_id )
)
WITH (
  OIDS=FALSE
);
ALTER TABLE dc_moi_attachment
  OWNER TO "IGTDefconUser";
COMMENT ON TABLE dc_moi_attachment
  IS '検船添付ファイルテーブル';

-- Trigger: tr_dc_moi_attachment on dc_moi_attachment

-- DROP TRIGGER tr_dc_moi_attachment ON dc_moi_attachment;

CREATE TRIGGER tr_dc_moi_attachment
  BEFORE INSERT OR UPDATE
  ON dc_moi_attachment
  FOR EACH ROW
  EXECUTE PROCEDURE "TrAutoDateUpdate"();

