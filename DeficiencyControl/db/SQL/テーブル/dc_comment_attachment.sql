-- Table: dc_comment_attachment

-- DROP TABLE dc_comment_attachment;

CREATE TABLE dc_comment_attachment
(
  comment_id integer NOT NULL,
  attachment_id integer NOT NULL,
  
  delete_flag boolean DEFAULT false,
  create_ms_user_id character varying(40),
  update_ms_user_id character varying(40),
  create_date timestamp without time zone,
  update_date timestamp without time zone,
  CONSTRAINT pk_dc_comment_attachment PRIMARY KEY (comment_id , attachment_id )
)
WITH (
  OIDS=FALSE
);
ALTER TABLE dc_comment_attachment
  OWNER TO "IGTDefconUser";
COMMENT ON TABLE dc_comment_attachment
  IS 'コメント親添付ファイルテーブル';

-- Trigger: tr_dc_comment_attachment on dc_comment_attachment

-- DROP TRIGGER tr_dc_comment_attachment ON dc_comment_attachment;

CREATE TRIGGER tr_dc_comment_attachment
  BEFORE INSERT OR UPDATE
  ON dc_comment_attachment
  FOR EACH ROW
  EXECUTE PROCEDURE "TrAutoDateUpdate"();

