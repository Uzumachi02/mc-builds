CREATE TABLE IF NOT EXISTS "public"."attachment_types" (
  "id" int4 NOT NULL,
  "name" varchar NOT NULL,
  "create_date" timestamp DEFAULT now(),
  PRIMARY KEY ("id")
)
;

INSERT INTO "public"."attachment_types" ("id", "name") VALUES (1, 'Link');
INSERT INTO "public"."attachment_types" ("id", "name") VALUES (2, 'Image');
INSERT INTO "public"."attachment_types" ("id", "name") VALUES (3, 'Video');