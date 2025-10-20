-- Table: dc_accident_attachment

-- DROP TABLE dc_accident_attachment;

CREATE TABLE dc_accident_attachment
(
  accident_id integer NOT NULL,
  attachment_id integer NOT NULL,
  
  delete_flag boolean DEFAULT false,
  create_ms_user_id character varying(40),
  update_ms_user_id character varying(40),
  create_date timestamp without time zone,
  update_date timestamp without time zone,
  CONSTRAINT pk_dc_accident_attachment PRIMARY KEY (accident_id , attachment_id )
)
WITH (
  OIDS=FALSE
);
ALTER TABLE dc_accident_attachment
  OWNER TO "IGTDefconUser";
COMMENT ON TABLE dc_accident_attachment
  IS '事故トラブル添付ファイルテーブル';

-- Trigger: tr_dc_accident_attachment on dc_accident_attachment

-- DROP TRIGGER tr_dc_accident_attachment ON dc_accident_attachment;

CREATE TRIGGER tr_dc_accident_attachment
  BEFORE INSERT OR UPDATE
  ON dc_accident_attachment
  FOR EACH ROW
  EXECUTE PROCEDURE "TrAutoDateUpdate"();

