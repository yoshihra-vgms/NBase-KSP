-- Table: dc_comment_item_attachment

-- DROP TABLE dc_comment_item_attachment;

CREATE TABLE dc_comment_item_attachment
(
  comment_item_id integer NOT NULL,
  attachment_id integer NOT NULL,
  
  delete_flag boolean DEFAULT false,
  create_ms_user_id character varying(40),
  update_ms_user_id character varying(40),
  create_date timestamp without time zone,
  update_date timestamp without time zone,
  CONSTRAINT pk_dc_comment_item_attachment PRIMARY KEY (comment_item_id , attachment_id )
)
WITH (
  OIDS=FALSE
);
ALTER TABLE dc_comment_item_attachment
  OWNER TO "IGTDefconUser";
COMMENT ON TABLE dc_comment_item_attachment
  IS 'コメントアイテム添付ファイルテーブル';

-- Trigger: tr_dc_comment_item_attachment on dc_comment_item_attachment

-- DROP TRIGGER tr_dc_comment_item_attachment ON dc_comment_item_attachment;

CREATE TRIGGER tr_dc_comment_item_attachment
  BEFORE INSERT OR UPDATE
  ON dc_comment_item_attachment
  FOR EACH ROW
  EXECUTE PROCEDURE "TrAutoDateUpdate"();

