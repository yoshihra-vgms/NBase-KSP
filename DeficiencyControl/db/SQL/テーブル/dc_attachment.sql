-- Table: dc_attachment

-- DROP TABLE dc_attachment;

CREATE TABLE dc_attachment
(
  attachment_id serial NOT NULL,
  filename character varying,
  icon_data bytea,
  file_data bytea,
  attachment_type_id integer,
  delete_flag boolean DEFAULT false,
  create_ms_user_id character varying(40),
  update_ms_user_id character varying(40),
  create_date timestamp without time zone,
  update_date timestamp without time zone,
  CONSTRAINT pk_dc_attachment PRIMARY KEY (attachment_id )
)
WITH (
  OIDS=FALSE
);
ALTER TABLE dc_attachment
  OWNER TO "IGTDefconUser";
COMMENT ON TABLE dc_attachment
  IS '添付ファイルテーブル';

-- Trigger: tr_dc_attachment on dc_attachment

-- DROP TRIGGER tr_dc_attachment ON dc_attachment;

CREATE TRIGGER tr_dc_attachment
  BEFORE INSERT OR UPDATE
  ON dc_attachment
  FOR EACH ROW
  EXECUTE PROCEDURE "TrAutoDateUpdate"();

