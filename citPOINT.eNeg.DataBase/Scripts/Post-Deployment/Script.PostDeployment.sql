/*-----------------------------------------------
				Country Script
-------------------------------------------------*/

IF NOT EXISTS (SELECT * FROM [dbo].[Country])
BEGIN
BEGIN TRANSACTION;
INSERT INTO [dbo].[Country]([CountryID], [CountryName])
SELECT N'c73c0960-c0f1-4a1f-a0f1-00dc7f0bbdff', N'Jamaica' UNION ALL
SELECT N'5c4117c8-56a2-41d6-9f88-01629fa3efbe', N'Kazakhstan' UNION ALL
SELECT N'a89874c3-0091-4707-963f-018cf393fd3e', N'Ivory Coast' UNION ALL
SELECT N'd0206a0e-c9c5-4ea6-ab0c-02d72fdf9102', N'Sweden' UNION ALL
SELECT N'053e9f03-08f4-4bfb-834e-0599f42ecc8e', N'Cocos (Keeling) Islands' UNION ALL
SELECT N'66b096d3-2a4c-4a18-b7bd-068f09c10995', N'Nigeria' UNION ALL
SELECT N'51d08b22-2ca9-41b6-98c9-07a0bc1b97e7', N'Pitcairn Islands' UNION ALL
SELECT N'1f0d6231-6495-4c2e-8146-0a6985585192', N'Fiji' UNION ALL
SELECT N'c89fc229-b18a-4dab-9db8-0b95faeacf18', N'San Marino' UNION ALL
SELECT N'69fa9a1f-0ff6-4090-b863-0d7a40924fac', N'Tonga' UNION ALL
SELECT N'6b4b51fb-8305-403d-a2e0-0e20958c8fa5', N'Federated States of Micronesia' UNION ALL
SELECT N'a2989d19-e4c1-4560-a407-0e45755d8150', N'Malawi' UNION ALL
SELECT N'b38f7abb-ba7d-4b14-aa44-0e83fa334a77', N'Tuvalu' UNION ALL
SELECT N'ba8da8e0-de82-4b1c-be34-1168cea2cb8f', N'Wallis and Futuna' UNION ALL
SELECT N'c58698c5-ba90-425e-bbbc-11868bd43b3f', N'Argentina' UNION ALL
SELECT N'156d11eb-d210-462d-9cd0-12ac9c16efc0', N'Botswana' UNION ALL
SELECT N'd030ab59-421a-4363-8cc8-1436d00cb76a', N'Suriname' UNION ALL
SELECT N'71eed38d-53b3-4d7c-b6ee-148af23f090c', N'Bhutan' UNION ALL
SELECT N'1a3d2336-8249-48f1-8136-15424144f963', N'India' UNION ALL
SELECT N'f2b2d755-e78d-4d25-9f26-16cb4950ea1a', N'Armenia' UNION ALL
SELECT N'd39f2893-39fa-48b3-8d41-1774391168dd', N'Uganda' UNION ALL
SELECT N'a807ff7e-9b7b-4519-b51b-17c53abd12b2', N'Kuwait' UNION ALL
SELECT N'8d301ebb-0137-4e21-9877-187e35bb333b', N'Montenegro' UNION ALL
SELECT N'b0d4b493-54ed-4165-a56e-18c7ee8ca9ec', N'Aruba' UNION ALL
SELECT N'b5b86682-7c27-4f1e-bc98-195592be9cf9', N'Venezuela' UNION ALL
SELECT N'0a66f783-9da3-4e7f-9217-19a37e1bc820', N'Norway' UNION ALL
SELECT N'8b8a2200-4349-477c-856d-1b770d03674e', N'Guinea' UNION ALL
SELECT N'748e5b4e-36c9-4d0c-a688-1d29d853df93', N'French Polynesia' UNION ALL
SELECT N'5754ef92-8e6b-4378-9b9b-1d5df5357648', N'Rwanda' UNION ALL
SELECT N'e88f8902-5394-4f53-b738-205b41b01031', N'Luxembourg' UNION ALL
SELECT N'075aead5-59e1-429e-ab6d-2097a4eed889', N'Aland Islands' UNION ALL
SELECT N'2533aea6-7bdb-4a4a-880c-213f96cebace', N'Solomon Islands' UNION ALL
SELECT N'87af9a97-ec79-46e0-ad18-21e99abd992b', N'Vanuatu' UNION ALL
SELECT N'f2e5d94f-a348-4848-9c62-22980cba9155', N'Thailand' UNION ALL
SELECT N'75c03c04-adf0-4f83-8360-2386d7296810', N'Saint Vincent and the Grenadines' UNION ALL
SELECT N'e95da4c4-e396-4e2b-902a-2474495ad455', N'Cayman Islands' UNION ALL
SELECT N'7fc168ac-5bf5-4740-ad11-24f3c303d216', N'Estonia' UNION ALL
SELECT N'a2b54829-59e2-4a73-bdbd-258b7a788f41', N'Namibia' UNION ALL
SELECT N'5420372c-2981-4231-a874-266b2fec16d8', N'Moldova' UNION ALL
SELECT N'c016b775-e056-4f96-acb0-268453563a63', N'Myanmar' UNION ALL
SELECT N'9e687c99-3d2e-45d6-a8fb-2818c7ac05da', N'Egypt' UNION ALL
SELECT N'0ae3274e-8dd2-48af-b2e0-28c53c019d47', N'Andorra' UNION ALL
SELECT N'a722bce8-9670-45e4-b91c-2a53bef8b4a5', N'Gibraltar' UNION ALL
SELECT N'c8329d25-b460-4ef7-86c1-2aaf43c05271', N'Malaysia' UNION ALL
SELECT N'efff8bda-41ee-4e4b-840e-2bac2c10b49f', N'Qatar' UNION ALL
SELECT N'f17ef2a3-650a-4a9b-8704-2cab925dcd15', N'Netherlands Antilles' UNION ALL
SELECT N'e84945ab-d4c2-4c10-808f-2e17cd07e9df', N'South Georgia and South Sandwich Islands' UNION ALL
SELECT N'23a742bb-2118-4aaf-acb2-2ef028242feb', N'Croatia' UNION ALL
SELECT N'0762cac4-fc98-4db4-8a27-30908a6e4a64', N'Zimbabwe' UNION ALL
SELECT N'5b2be425-a6cc-4bfe-98a1-32aca27d076a', N'Slovakia'  UNION ALL
SELECT N'120b2931-7edb-4fe1-a595-32c2e5f5dc1c', N'Paraguay' UNION ALL
SELECT N'13750f99-724a-4faf-aa49-3319c9fd207d', N'Canada' UNION ALL
SELECT N'b5edf2da-6fda-46ff-9c15-33206984b871', N'Pakistan' UNION ALL
SELECT N'c8657016-55c5-486d-8282-347f980ff055', N'Mexico' UNION ALL
SELECT N'09c77437-23df-49f1-ab53-352aa30f2b31', N'Vatican City' UNION ALL
SELECT N'3dc38b7b-38f7-4b2d-ba7d-35fd1f39df19', N'Belize' UNION ALL
SELECT N'a4dbae0f-bbe7-4e41-af96-364c7f39c897', N'Macau' UNION ALL
SELECT N'b6c60f78-affa-4395-b192-3820bd219dec', N'Japan' UNION ALL
SELECT N'e3efd8fe-23b5-43cf-92bb-38b648d5dc0e', N'Yemen' UNION ALL
SELECT N'09d32f7e-d0f0-48e1-b99f-3b80c4e3b9e7', N'Equatorial Guinea' UNION ALL
SELECT N'0ac17d88-f4ac-4dab-9ee1-3c8d3a82d5c1', N'Palau' UNION ALL
SELECT N'ad306f3c-8afa-45e2-8dbe-3ce92ce4069c', N'Australia' UNION ALL
SELECT N'34211baf-75e8-4712-9ba0-3d803e6d274a', N'Bulgaria' UNION ALL
SELECT N'a5ef8164-8954-4765-b97a-415bd6de80b2', N'Mayotte' UNION ALL
SELECT N'aadc9080-4aed-48cd-8449-41ac36b6b604', N'Bouvet Island' UNION ALL
SELECT N'2ff0214d-ff8b-4dcd-99de-41f957719079', N'Nauru' UNION ALL
SELECT N'2f95e9bb-1d07-44c8-af29-4206ea33b39e', N'Tajikistan' UNION ALL
SELECT N'e37ffe1c-2f93-45cb-b8c5-420ca9d2b7c0', N'Albania' UNION ALL
SELECT N'eb842de4-d9a9-48de-99c7-430126cef237', N'Cuba' UNION ALL
SELECT N'bed8694d-f721-437c-84c8-43433f22cdea', N'Antigua and Barbuda' UNION ALL
SELECT N'577875b0-ee16-4e51-bbd9-45a7d44651a7', N'Uruguay' UNION ALL
SELECT N'fc70602c-539f-4fa8-8f36-46fd752ee401', N'Uzbekistan' UNION ALL
SELECT N'33183dbf-29a5-4ec1-b0f7-486906b36b06', N'Spratly Islands' UNION ALL
SELECT N'79a16802-a4b3-4d8b-98bd-497ea6dc1f36', N'Ukraine' UNION ALL
SELECT N'1ecc8b45-baa5-44f2-bc23-4a4ba9894513', N'Comoros' UNION ALL
SELECT N'6e8be570-d486-4a4a-aa7c-4bbe83cb12a3', N'East Timor' UNION ALL
SELECT N'19d6c8a0-c11b-40c3-80f7-4be1b0ff915a', N'Bosnia and Herzegovina' UNION ALL
SELECT N'34da950e-ac91-47a0-bc1c-4be567e4a4b1', N'Tunisia' UNION ALL
SELECT N'95f1a651-6df7-4553-954e-4cc3bb8755a9', N'South Korea' UNION ALL
SELECT N'4b4449b8-9af1-41d8-a132-4cc4a6afb894', N'Czech Republic' UNION ALL
SELECT N'b63a7405-060a-46fa-9cf1-4d48ca3350e8', N'Central African Republic' UNION ALL
SELECT N'682cd21c-63a1-4214-ad83-4e4f26b95547', N'United States Minor Outlying Islands' UNION ALL
SELECT N'8886fbe4-f93a-4381-a2b0-51e9d7c36236', N'Saint Helena and Dependencies' UNION ALL
SELECT N'2ef95b6d-3d56-4781-b2be-5221059d60f1', N'Niger' UNION ALL
SELECT N'65ea8544-ef41-45f1-8109-52737e62f800', N'French Southern Territories' UNION ALL
SELECT N'e0935c24-eadd-48eb-8f66-52e9b5f6612d', N'Sao Tome and Principe' UNION ALL
SELECT N'd308b89e-bac4-4934-9e11-54cfd14e2dec', N'Brunei' UNION ALL
SELECT N'2bb54a68-a71c-4338-9de8-54e58aae0fd9', N'Mozambique' UNION ALL
SELECT N'9bc95571-b16c-40ed-b278-56fa431e0bd4', N'North Korea' UNION ALL
SELECT N'6a6e7395-5d2d-4e57-8251-5742c49e4597', N'Panama' UNION ALL
SELECT N'11d728b8-6638-4e3f-b732-58369c6d1089', N'Cameroon' UNION ALL
SELECT N'e27e6548-c3d8-42cc-99f6-5920a128a95c', N'Denmark' UNION ALL
SELECT N'44c12f42-1a8c-45a2-ac4a-59761b89c087', N'Guyana' UNION ALL
SELECT N'6f0ea453-df5c-4f62-adcf-59e7357f5593', N'Antarctica' UNION ALL
SELECT N'35f78f6f-5c76-4668-b5c2-59ff02501405', N'Chile' UNION ALL
SELECT N'c4f8a97c-d026-416a-836c-5ae1f8e6fb4d', N'Niue' UNION ALL
SELECT N'64196b8e-baaa-4494-b882-5bdaca977433', N'Maldives' UNION ALL
SELECT N'0edcdc1c-2353-4b2e-ab25-5d54ac0d7e80', N'Haiti' UNION ALL
SELECT N'f35e1c9d-a9e9-45c4-b9e4-5dade8d05a5a', N'Sierra Leone' UNION ALL
SELECT N'23bc33e5-acf7-4467-994f-5e37a7953513', N'Hungary'  UNION ALL
SELECT N'7b5cb44f-50df-40a4-a0cc-601de3bc9a05', N'Falkland Islands' UNION ALL
SELECT N'9b267000-3d95-4a24-b720-624df7b33126', N'New Zealand' UNION ALL
SELECT N'8a5e249c-42af-4291-ae2a-62cabbb69e2e', N'Hong Kong' UNION ALL
SELECT N'cddf0fd1-5584-4068-bfd9-638560221ebc', N'Bolivia' UNION ALL
SELECT N'4ae5dcf0-2f66-4b85-b525-63c6aaa24254', N'Iran' UNION ALL
SELECT N'ee2d803c-8e56-428d-bc1c-64997a2d5944', N'Afghanistan' UNION ALL
SELECT N'fff32f82-a3c2-406b-92ef-649c5a36a9c5', N'Netherlands' UNION ALL
SELECT N'ad22b6cc-32d6-482a-b8b0-64a0caf552d5', N'Germany' UNION ALL
SELECT N'98b1975c-df2c-4d34-975a-65778df08ac3', N'Mali' UNION ALL
SELECT N'dc354312-acdb-49a1-a5a3-66ef1076cb26', N'Northern Mariana Islands' UNION ALL
SELECT N'250e28f2-faa1-4b1a-956c-66f4d87b7980', N'Cape Verde' UNION ALL
SELECT N'8be10fb5-1552-4b0d-9e7e-6830abfff53e', N'Turks And Caicos Islands' UNION ALL
SELECT N'a3903c57-0d28-4e2c-9af9-6a00fb46088f', N'France' UNION ALL
SELECT N'df39fb64-ec78-456b-a478-6a96e29de4e0', N'Romania' UNION ALL
SELECT N'dd2255c4-3830-445d-bf4e-6b28a930e9e0', N'Greece' UNION ALL
SELECT N'a4c08a98-22ed-438f-ad49-6d9ad95cd461', N'Mongolia' UNION ALL
SELECT N'fca4d37c-203c-4f2c-b8a1-6e6a282b5141', N'Spain' UNION ALL
SELECT N'a677b518-d254-4ecb-aadf-711193f300c1', N'Burkina Faso' UNION ALL
SELECT N'd678becc-aaad-4133-b654-789fcccbcd9d', N'Kenya' UNION ALL
SELECT N'767a9865-9afc-4e49-9585-7901dd8823dd', N'Kiribati' UNION ALL
SELECT N'f937e43e-c5a4-49c1-95a8-79727242d8ea', N'Peru' UNION ALL
SELECT N'00d98288-05f5-4da7-9852-797d51ea6bbb', N'Italy' UNION ALL
SELECT N'0676039e-7ade-4a84-8110-7ab912196dc7', N'Jordan' UNION ALL
SELECT N'd9e846a6-d1a0-4b31-880e-7b82c233d96a', N'Palestinian Occupied Territories' UNION ALL
SELECT N'3067b9c1-7fea-4d77-8994-7c6cd6bc9b41', N'US Virgin Islands' UNION ALL
SELECT N'48d0d3f0-31e9-44b5-b782-7f4605444662', N'Anguilla' UNION ALL
SELECT N'893c2fac-7fad-4dcf-a925-804e449df16e', N'United Kingdom' UNION ALL
SELECT N'ab213555-5ebc-4bfe-977a-80fcdddbb538', N'Finland' UNION ALL
SELECT N'a63be64e-19cc-41cd-b4a0-821749be604b', N'China' UNION ALL
SELECT N'f003fe36-9777-4a5a-b67c-859fc36f5814', N'Saint Pierre and Miquelon' UNION ALL
SELECT N'febeb2a0-45c7-40b0-9f76-85a2966461da', N'New Caledonia' UNION ALL
SELECT N'd9117ce8-2a2c-4523-ad2c-85ce8a2ae7e0', N'Benin' UNION ALL
SELECT N'5947cb47-729d-402f-bade-8a739a083cfe', N'Burundi' UNION ALL
SELECT N'0a1e662f-1fe9-4b29-b787-8bd8cc192f17', N'Georgia' UNION ALL
SELECT N'456fdf17-459f-4c65-944f-8f5117d50fa2', N'Libya' UNION ALL
SELECT N'c9515be0-85ac-46d5-989e-8fb1f7cce0e0', N'Lesotho' UNION ALL
SELECT N'0da5beda-b73a-4375-b87d-903f20c426e3', N'Lithuania' UNION ALL
SELECT N'011bb241-abaf-456e-a261-9215c7629b01', N'Dominican Republic' UNION ALL
SELECT N'72dfba1d-10d8-4f0a-b65d-942fba57ddeb', N'United Arab Emirates' UNION ALL
SELECT N'761800fd-1be5-4bb0-9820-955b24b6c398', N'Eritrea' UNION ALL
SELECT N'c94252c2-4965-4c5b-85e0-95f7c4498cab', N'Tokelau' UNION ALL
SELECT N'05b9849a-5c46-4230-868b-968d1bf874ab', N'Algeria' UNION ALL
SELECT N'e32ad0dc-cb28-42b5-b1ca-9690ba8fb4a8', N'Poland' UNION ALL
SELECT N'b9b3f383-2cee-4004-9163-986114592629', N'Oman' UNION ALL
SELECT N'deec1eb1-61fd-4972-9ee3-98c2a35ecdf0', N'Serbia' UNION ALL
SELECT N'b82cfe34-2f1b-454c-be47-98d6a4de0e99', N'Svalbard and Jan Mayen' UNION ALL
SELECT N'd4a573ad-0f23-4e7b-ac6d-9a2a28894b07', N'Ecuador' UNION ALL
SELECT N'5c8cdbe0-34bd-4097-81c8-9a87df54ff97', N'Austria' UNION ALL
SELECT N'0a153053-dd51-4d4c-9ca3-9ac1b8b1232d', N'American Samoa' UNION ALL
SELECT N'f4721e89-1364-4d8e-98f5-9bc6c104a8ee', N'Zambia' UNION ALL
SELECT N'9fec4f1b-5572-4a44-b310-9c098b46d947', N'Vietnam' UNION ALL
SELECT N'adcd726d-5594-4cb6-a5fe-9cca47554f2b', N'Indonesia' UNION ALL
SELECT N'72b51b3c-8b96-47be-9cd4-9cea8a5d3f9a', N'Djibouti' UNION ALL
SELECT N'35289e2d-a821-4382-8087-9d87aa8c80e9', N'Saint Kitts and Nevis' UNION ALL
SELECT N'ac6f19de-d921-44e4-b649-a0f5920fc59f', N'Switzerland' UNION ALL
SELECT N'5584e503-ef7d-405d-bf13-a1149f118f30', N'Cambodia' UNION ALL
SELECT N'd06a7775-65f3-4ca8-9b5d-a2537a2af062', N'Israel' UNION ALL
SELECT N'78852f08-bd48-41d1-a028-a29d2852c33b', N'Slovenia' UNION ALL
SELECT N'3ebb6279-2af2-4e6d-b34e-a2d7cf694f13', N'Latvia' UNION ALL
SELECT N'ff1dee95-80e8-414a-b9b7-a3b877f0dcd9', N'Bahrain' UNION ALL
SELECT N'3cd6c791-34fd-44c6-b0ed-a65edd21b116', N'Macedonia' UNION ALL
SELECT N'de14894a-9ce1-424f-895b-a6f59a60d178', N'Bahamas' UNION ALL
SELECT N'1dae0012-faa4-4c0d-8b62-a74bf57c16b1', N'Iraq' UNION ALL
SELECT N'e0b52b1b-a550-415a-a312-a8d3d7345ca8', N'Trinidad and Tobago' UNION ALL
SELECT N'79212f68-fa4a-44de-942d-a990bd47b5d9', N'Barbados' UNION ALL
SELECT N'08404259-0e0e-4126-b2af-a9953390d8d6', N'Belarus' UNION ALL
SELECT N'15c80c5c-3b0d-4a51-b579-abb094dc58ee', N'Taiwan' UNION ALL
SELECT N'1a33931d-c829-4d2a-9206-aca7f7f1851f', N'Ireland' UNION ALL
SELECT N'b6842c07-1fac-41aa-9709-accd02eb340d', N'Dominica' UNION ALL
SELECT N'eba4b280-9f36-465d-91ef-b0a2d5659c02', N'Greenland' UNION ALL
SELECT N'a3b57b9f-fb9b-4710-9065-b17c54af5191', N'Ghana' UNION ALL
SELECT N'4c999312-1977-4d1f-8910-b23e8b812a8a', N'Marshall Islands' UNION ALL
SELECT N'2d247a40-ff4b-430e-ab7e-b35c83f3fde2', N'Malta' UNION ALL
SELECT N'9c3215bc-2b59-4e88-aeee-b3c670209c56', N'Guinea-Bissau' UNION ALL
SELECT N'84f28cf1-e529-45f7-abd5-b470652ed581', N'Costa Rica' UNION ALL
SELECT N'27abcf1f-823a-40d0-afe8-b5c84112a6fc', N'Turkmenistan' UNION ALL
SELECT N'888ab9f0-e9c3-4078-8ff5-b61a3d8b85a1', N'French Guyana' UNION ALL
SELECT N'c14890be-95e9-4c08-aaf6-b78d684c0ed5', N'Bermuda' UNION ALL
SELECT N'9643db52-75a8-427a-927b-ba97c3614fdf', N'El Salvador' UNION ALL
SELECT N'e4a826c1-23f8-4def-a38c-bad844b31c44', N'Colombia' UNION ALL
SELECT N'565fbd0a-60a1-4b58-b912-bb079952f4f7', N'Liechtenstein' UNION ALL
SELECT N'955ba606-e427-4002-9497-bc385dbab4a9', N'Mauritania' UNION ALL
SELECT N'2241bca4-d3b6-40b1-bec8-be9ea95e1313', N'Madagascar' UNION ALL
SELECT N'eab934f5-9611-431e-924b-bf8ea2a41630', N'Montserrat' UNION ALL
SELECT N'4ae30e46-2b4a-4e65-a878-bfcf39e7a460', N'Angola' UNION ALL
SELECT N'172eeed8-3323-49c5-8e70-c13d5ecb53b6', N'Russia' UNION ALL
SELECT N'ebec64b4-fb5c-462d-be3e-c1693d51057a', N'United States' UNION ALL
SELECT N'761f8169-a4ff-4ac8-8d82-c169eaac1f0b', N'Liberia' UNION ALL
SELECT N'1bcdb11e-1d0f-4dd1-9f22-c19580b7c65f', N'Ethiopia' UNION ALL
SELECT N'c69163e1-69a3-456f-ae73-c1f2164a9523', N'Swaziland' UNION ALL
SELECT N'72f62a14-1be8-4e8d-90cd-c295f62cc9b6', N'Kyrgyzstan' UNION ALL
SELECT N'9c200ed1-6880-4a4f-b425-c4efb6b7a266', N'Belgium' UNION ALL
SELECT N'f3672bb3-9577-4d99-8b82-c5a37798f588', N'Faroe Islands' UNION ALL
SELECT N'1789b802-fa39-476b-a488-c6c70762e58a', N'Togo' UNION ALL
SELECT N'50f98ee5-fe75-4db1-9b37-c7e583d1acd9', N'Saint Lucia' UNION ALL
SELECT N'ba1d4242-0db5-4f39-9c9e-c8206b44a2ac', N'Disputed Territory' UNION ALL
SELECT N'1cef7729-a6b6-4e0f-bc96-c8bad10bad2a', N'Turkey' UNION ALL
SELECT N'7d705125-bfe8-4fe8-b0b2-ca5f62c4795c', N'Sudan' UNION ALL
SELECT N'3c877121-2b15-46a7-8f06-ca761ce3a7de', N'Guatemala' UNION ALL
SELECT N'4127791e-9475-4a15-aa93-cd382879c268', N'Heard Island and Mcdonald Islands' UNION ALL
SELECT N'ccb6bc5f-edcd-4d1c-9fa8-cd98a1972ad4', N'Portugal' UNION ALL
SELECT N'645f5fa3-9aed-4234-a1d7-ce259ff27d89', N'Somalia' UNION ALL
SELECT N'20f816ce-d52e-4883-8446-ce7a4b3af307', N'Senegal' UNION ALL
SELECT N'70d72932-14e0-403d-b010-cfeaf14c821c', N'Cyprus' UNION ALL
SELECT N'357d7022-327b-41c7-a9b1-d062ed9c01f9', N'Tanzania' UNION ALL
SELECT N'aadeb659-846a-46dd-9bb8-d46a63e64830', N'Brazil' UNION ALL
SELECT N'2a4f1bb0-2dd7-40f6-b2a8-dbe8759f7bd0', N'Western Sahara' UNION ALL
SELECT N'4d0e1def-248f-41ef-9807-dd753e4511c8', N'British Indian Ocean Territory' UNION ALL
SELECT N'7820f9dc-805d-437a-8422-de54950c0fe3', N'Singapore' UNION ALL
SELECT N'7d0a4f35-3ac3-4e39-9fd1-e09e403835a3', N'Christmas Island' UNION ALL
SELECT N'1b960757-5764-439e-91f3-e1ed5d99f99e', N'Reunion' UNION ALL
SELECT N'f7b983dd-5721-4e04-8a12-e3ebd3f69e07', N'Democratic Republic of Congo' UNION ALL
SELECT N'0ba0cce0-ee01-4caa-8f3e-e52434b0890a', N'Papua New Guinea' UNION ALL
SELECT N'93000c72-7a62-438c-8c99-e527bba55ff1', N'Philippines' UNION ALL
SELECT N'dd4b9c7a-d6f7-45c4-9bfe-e72a17144f6b', N'Mauritius' UNION ALL
SELECT N'b5c4bb89-dc5f-47bd-9c16-e892dcb33015', N'Laos' UNION ALL
SELECT N'd7e5f80f-be0c-4a50-90c3-e8c21c557053', N'Saudi Arabia' UNION ALL
SELECT N'96a368ee-cd0d-43ff-920e-ea3661129696', N'British Virgin Islands' UNION ALL
SELECT N'90e9f1cb-6039-4689-b804-eab6723a5d2c', N'Chad' UNION ALL
SELECT N'8307f94f-f674-4048-9394-eabcdf9d896e', N'Guam' UNION ALL
SELECT N'5a0e13b9-2742-4494-b7b8-ebd2cc9cc3e2', N'Iceland' UNION ALL
SELECT N'65cabdc4-36f2-4d47-89e8-ed415ed8362c', N'Grenada' UNION ALL
SELECT N'd063630d-62ea-498a-b044-edc5642cb14b', N'Seychelles' UNION ALL
SELECT N'c35af5c8-11df-49ca-80da-ee3bfed7b0cf', N'Honduras' UNION ALL
SELECT N'6a8aad9a-542d-4d18-8b06-ef2bf89c0689', N'Guadeloupe' UNION ALL
SELECT N'9e0cbab0-c1bf-4659-9293-efab9de36ead', N'Azerbaijan' UNION ALL
SELECT N'86ee2565-1c2d-4067-b246-efdc586dfbd7', N'Norfolk Island' UNION ALL
SELECT N'19ca4002-9ce4-4899-aa81-f177c04c94f2', N'South Africa' UNION ALL
SELECT N'15834f7d-59de-4455-87f1-f2e8d4aaa645', N'Iraq-Saudi Arabia Neutral Zone' UNION ALL
SELECT N'a08974e5-efa4-409a-bc7b-f38967374386', N'Puerto Rico' UNION ALL
SELECT N'72e79f75-221f-4dae-aa26-f3aa24bd947f', N'Gambia' UNION ALL
SELECT N'7c10c38f-021b-48a5-85f2-f65a16c10009', N'Syria' UNION ALL
SELECT N'2ed8db5f-b98b-4fba-9f9b-f6e6a9e016d4', N'Nicaragua' UNION ALL
SELECT N'1999c1ce-89e1-4248-a7e9-f791ca684d0d', N'Samoa' UNION ALL
SELECT N'c91a5c71-7405-4caf-8978-f89f8ec3a861', N'Sri Lanka' UNION ALL
SELECT N'c12d9eec-7659-4de1-88d2-f8a142cfdac0', N'United Nations Neutral Zone' UNION ALL
SELECT N'20483b69-28ef-47af-9503-fa09d3261f6e', N'Nepal' UNION ALL
SELECT N'd1f121a5-b188-46fb-83e2-fba55ec2fa45', N'Monaco' UNION ALL
SELECT N'8ca589e1-89eb-40cc-bca7-fc3dae7c59c3', N'Martinique' UNION ALL
SELECT N'b6572364-63d1-47d5-a9c0-fd07177347b5', N'Lebanon' UNION ALL
SELECT N'3a27dbf3-13de-40c2-9f1d-fd2bd2787943', N'Morocco' UNION ALL
SELECT N'd0dc408f-5c04-4877-a7d8-fe96444b33a2', N'Cook Islands' UNION ALL
SELECT N'1488edf8-8999-423d-97d2-fea4f94ebce1', N'Bangladesh' UNION ALL
SELECT N'65fc8b2f-d12c-473a-bd5b-feef3835e6da', N'Congo' UNION ALL
SELECT N'caacd510-4c85-493a-8aa6-ff6e2c5ea17b', N'Gabon'
COMMIT;
RAISERROR (N'[dbo].[Country]: Insert Batch: 5.....Done!', 10, 1) WITH NOWAIT;
END
GO


/*-----------------------------------------------
		[PreferedLanguage] Script
-------------------------------------------------*/

IF NOT EXISTS (SELECT * FROM [dbo].[PreferedLanguage])
BEGIN
BEGIN TRANSACTION;
INSERT INTO [dbo].[PreferedLanguage]([LCID], [PreferedLanguage])
SELECT 1025, N'Arabic - Saudi Arabia' UNION ALL
SELECT 1026, N'Bulgarian' UNION ALL
SELECT 1027, N'Catalan' UNION ALL
SELECT 1028, N'Chinese - Taiwan' UNION ALL
SELECT 1029, N'Czech' UNION ALL
SELECT 1030, N'Danish' UNION ALL
SELECT 1031, N'German - Germany' UNION ALL
SELECT 1032, N'Greek' UNION ALL
SELECT 1033, N'English - United States' UNION ALL
SELECT 1034, N'Spanish - Spain' UNION ALL
SELECT 1035, N'Finnish' UNION ALL
SELECT 1036, N'French - France' UNION ALL
SELECT 1037, N'Hebrew' UNION ALL
SELECT 1038, N'Hungarian' UNION ALL
SELECT 1039, N'Icelandic' UNION ALL
SELECT 1040, N'Italian - Italy' UNION ALL
SELECT 1041, N'Japanese' UNION ALL
SELECT 1042, N'Korean' UNION ALL
SELECT 1043, N'Dutch - the Netherlands' UNION ALL
SELECT 1044, N'Norwegian - Bokmål' UNION ALL
SELECT 1045, N'Polish' UNION ALL
SELECT 1046, N'Portuguese - Brazil' UNION ALL
SELECT 1047, N'Raeto-Romance' UNION ALL
SELECT 1048, N'Romanian - Romania' UNION ALL
SELECT 1049, N'Russian' UNION ALL
SELECT 1050, N'Croatian' UNION ALL
SELECT 1051, N'Slovak' UNION ALL
SELECT 1052, N'Albanian' UNION ALL
SELECT 1053, N'Swedish - Sweden' UNION ALL
SELECT 1054, N'Thai' UNION ALL
SELECT 1055, N'Turkish' UNION ALL
SELECT 1056, N'Urdu' UNION ALL
SELECT 1057, N'Indonesian' UNION ALL
SELECT 1058, N'Ukrainian' UNION ALL
SELECT 1059, N'Belarusian' UNION ALL
SELECT 1060, N'Slovenian' UNION ALL
SELECT 1061, N'Estonian' UNION ALL
SELECT 1062, N'Latvian' UNION ALL
SELECT 1063, N'Lithuanian' UNION ALL
SELECT 1065, N'Farsi' UNION ALL
SELECT 1066, N'Vietnamese' UNION ALL
SELECT 1067, N'Armenian' UNION ALL
SELECT 1068, N'Azeri - Latin' UNION ALL
SELECT 1069, N'Basque' UNION ALL
SELECT 1070, N'Sorbian' UNION ALL
SELECT 1071, N'FYRO Macedonian' UNION ALL
SELECT 1072, N'Sutu' UNION ALL
SELECT 1073, N'Tsonga' UNION ALL
SELECT 1074, N'Setsuana' UNION ALL
SELECT 1076, N'Xhosa' UNION ALL
SELECT 1077, N'Zulu' UNION ALL
SELECT 1078, N'Afrikaans' UNION ALL
SELECT 1080, N'Faroese' UNION ALL
SELECT 1081, N'Hindi' UNION ALL
SELECT 1082, N'Maltese' UNION ALL
SELECT 1084, N'Gaelic - Scotland' UNION ALL
SELECT 1085, N'Yiddish' UNION ALL
SELECT 1086, N'Malay - Malaysia' UNION ALL
SELECT 1089, N'Swahili' UNION ALL
SELECT 1091, N'Uzbek – Latin' UNION ALL
SELECT 1092, N'Tatar' UNION ALL
SELECT 1097, N'Tamil' UNION ALL
SELECT 1102, N'Marathi' UNION ALL
SELECT 1103, N'Sanskrit' UNION ALL
SELECT 2049, N'Arabic - Iraq' UNION ALL
SELECT 2052, N'Chinese - China' UNION ALL
SELECT 2055, N'German - Switzerland' UNION ALL
SELECT 2057, N'English - United Kingdom' UNION ALL
SELECT 2058, N'Spanish - Mexico' UNION ALL
SELECT 2060, N'French - Belgium' UNION ALL
SELECT 2064, N'Italian - Switzerland' UNION ALL
SELECT 2067, N'Dutch - Belgium' UNION ALL
SELECT 2068, N'Norwegian - Nynorsk' UNION ALL
SELECT 2070, N'Portuguese - Portugal' UNION ALL
SELECT 2072, N'Romanian - Moldova' UNION ALL
SELECT 2073, N'Russian - Moldova' UNION ALL
SELECT 2074, N'Serbian - Latin' UNION ALL
SELECT 2077, N'Swedish - Finland' UNION ALL
SELECT 2092, N'Azeri - Cyrillic' UNION ALL
SELECT 2108, N'Gaelic - Ireland' UNION ALL
SELECT 2110, N'Malay – Brunei' UNION ALL
SELECT 2115, N'Uzbek - Cyrillic' UNION ALL
SELECT 3073, N'Arabic - Egypt' UNION ALL
SELECT 3076, N'Chinese - Hong Kong SAR' UNION ALL
SELECT 3079, N'German - Austria' UNION ALL
SELECT 3081, N'English - Australia' UNION ALL
SELECT 3084, N'French - Canada' UNION ALL
SELECT 3098, N'Serbian - Cyrillic' UNION ALL
SELECT 4097, N'Arabic - Libya' UNION ALL
SELECT 4100, N'Chinese - Singapore' UNION ALL
SELECT 4103, N'German - Luxembourg' UNION ALL
SELECT 4105, N'English - Canada' UNION ALL
SELECT 4106, N'Spanish - Guatemala' UNION ALL
SELECT 4108, N'French - Switzerland' UNION ALL
SELECT 5121, N'Arabic - Algeria' UNION ALL
SELECT 5124, N'Chinese - Macau SAR' UNION ALL
SELECT 5127, N'German - Liechtenstein' UNION ALL
SELECT 5129, N'English - New Zealand' UNION ALL
SELECT 5130, N'Spanish - Costa Rica' UNION ALL
SELECT 5132, N'French - Luxembourg' UNION ALL
SELECT 6145, N'Arabic - Morocco' UNION ALL
SELECT 6153, N'English - Ireland' UNION ALL
SELECT 6154, N'Spanish - Panama' UNION ALL
SELECT 7169, N'Arabic - Tunisia' UNION ALL
SELECT 7177, N'English - South Africa' UNION ALL
SELECT 7178, N'Spanish - Dominican Republic' UNION ALL
SELECT 8193, N'Arabic - Oman' UNION ALL
SELECT 8201, N'English - Jamaica' UNION ALL
SELECT 8202, N'Spanish - Venezuela' UNION ALL
SELECT 9217, N'Arabic - Yemen' UNION ALL
SELECT 9225, N'English - Caribbean' UNION ALL
SELECT 9226, N'Spanish - Colombia' UNION ALL
SELECT 10241, N'Arabic - Syria' UNION ALL
SELECT 10249, N'English - Belize' UNION ALL
SELECT 10250, N'Spanish - Peru' UNION ALL
SELECT 11265, N'Arabic - Jordan' UNION ALL
SELECT 11273, N'English - Trinidad' UNION ALL
SELECT 11274, N'Spanish - Argentina' UNION ALL
SELECT 12289, N'Arabic - Lebanon' UNION ALL
SELECT 12298, N'Spanish - Ecuador' UNION ALL
SELECT 13313, N'Arabic - Kuwait' UNION ALL
SELECT 13321, N'English - Phillippines' UNION ALL
SELECT 13322, N'Spanish - Chile' UNION ALL
SELECT 14337, N'Arabic - United Arab Emirates' UNION ALL
SELECT 14346, N'Spanish - Uruguay' UNION ALL
SELECT 15361, N'Arabic - Bahrain' UNION ALL
SELECT 15370, N'Spanish - Paraguay' UNION ALL
SELECT 16385, N'Arabic - Qatar' UNION ALL
SELECT 16394, N'Spanish - Bolivia' UNION ALL
SELECT 17418, N'Spanish - El Salvador' UNION ALL
SELECT 18442, N'Spanish - Honduras' UNION ALL
SELECT 19466, N'Spanish - Nicaragua' UNION ALL
SELECT 20490, N'Spanish - Puerto Rico'
COMMIT;
RAISERROR (N'[dbo].[PreferedLanguage]: Insert Batch: 3.....Done!', 10, 1) WITH NOWAIT;
END
go

/*-----------------------------------------------
	[SecurityQuestion] Script
-------------------------------------------------*/

IF NOT EXISTS (SELECT * FROM [dbo].[SecurityQuestion])
BEGIN
BEGIN TRANSACTION;
INSERT INTO [dbo].[SecurityQuestion]([SecurityQuestionID], [Question])
SELECT N'f7d403ad-f6a5-43ea-89c0-1e0ec5e3118c', N'What is the name of your favorite sports team?' UNION ALL
SELECT N'6ff421f0-5ddf-4e49-9097-1ff78658fb01', N'What is the last name of your maid of honor at your wedding?' UNION ALL
SELECT N'bbfab559-bf77-4e42-81c8-28339ad00c18', N'What is the last name of your favorite musician?' UNION ALL
SELECT N'c8157f1d-6420-4a92-a339-28c91633e495', N'Where did you spend your childhood summers?' UNION ALL
SELECT N'd2ce228e-0511-4864-84c2-3063dc4b88cb', N'What was the make of your first motorcycle?' UNION ALL
SELECT N'a768e877-5f21-4df7-befd-30f1298ce9b0', N'What is the name of your favorite book?' UNION ALL
SELECT N'c8a5cdc3-7265-4109-ad61-3d89937333bc', N'What is the last name of your best man at your wedding?' UNION ALL
SELECT N'2c2233d4-50da-46d5-ad37-41d5d38ad197', N'What is the name of the hospital where you were born?' UNION ALL
SELECT N'83539ac8-40d8-4d87-a5b9-4e0d8a7d246f', N'What is the name of the street on which you grew up?' UNION ALL
SELECT N'2d33eb21-3aa0-40f2-8be2-50a2a61da395', N'What is the first name of your favorite aunt?' UNION ALL
SELECT N'f0b65c6e-a7d0-4d9c-a9b4-5bdec097ed9e', N'What was the last name of your favorite teacher?' UNION ALL
SELECT N'8569720f-521d-458f-9821-675c55801c18', N'What is your oldest cousin''s name?' UNION ALL
SELECT N'5a537be7-f963-473c-a966-8649bb240122', N'What is your youngest child''s nickname?' UNION ALL
SELECT N'831cbc5e-1de4-4984-bf06-928ea28d34c7', N'Who is your favorite author?' UNION ALL
SELECT N'2d8e2f50-b2d3-411d-8a65-a29c29bb4243', N'What is your main frequent flier number?' UNION ALL
SELECT N'4e0a87ea-c262-4fec-91e3-a3096fd907c0', N'What was the last name of your best childhood friend?' UNION ALL
SELECT N'174edd28-a2b9-435c-a603-a70f57daa8fd', N'What was the last name of your first boss?' UNION ALL
SELECT N'73467893-18cc-4e9f-a37c-a84fe8a8e997', N'What is the first name of your oldest niece?' UNION ALL
SELECT N'c1ef87c6-4a16-4537-b825-aa95b6b926f6', N'Where did you spend your honeymoon?' UNION ALL
SELECT N'c5ce155f-9da4-43ff-8669-b378e4027086', N'Who is your all-time favorite movie character?' UNION ALL
SELECT N'6e08b76b-b39a-45c6-afb0-cc2f9e959dae', N'What is your oldest child''s nickname?' UNION ALL
SELECT N'51e45445-8a20-41ff-a551-d250adad1a36', N'What was the make of your first car?' UNION ALL
SELECT N'b84d75a9-d311-424a-964a-dee498ee8fa3', N'What is the first name of your oldest nephew?' UNION ALL
SELECT N'5000b0a7-7140-403f-9187-e9857b58dfa3', N'What was your first pet&#39;s name?' UNION ALL
SELECT N'e5e4f602-393d-4424-aa2f-eb7c47a74f36', N'What was your favorite food as a child?' UNION ALL
SELECT N'8e9284b3-2012-4429-813c-fb7539a2d6fc', N'What is the first name of your favorite uncle?' UNION ALL
SELECT N'ee9c4eb4-bf62-4a9f-817b-ffcc61271071', N'Where did you meet your spouse?'
COMMIT;
RAISERROR (N'[dbo].[SecurityQuestion]: Insert Batch: 1.....Done!', 10, 1) WITH NOWAIT;
END
GO

/*-----------------------------------------------
	[ChannelType] Script
-------------------------------------------------*/

IF NOT EXISTS (SELECT * FROM [dbo].[ChannelType])
BEGIN
BEGIN TRANSACTION;
INSERT INTO [dbo].[ChannelType]([ChannelTypeID], [TypeName])
SELECT N'7529415a-bcc7-448b-815e-af522a504ed7', N'Default Type' UNION ALL
SELECT N'01862435-6783-43D3-B454-2DF645EB2875', N'Apps Type'
COMMIT;
RAISERROR (N'[dbo].[ChannelType]: Insert Batch: 1.....Done!', 10, 1) WITH NOWAIT;
END
GO

/*-----------------------------------------------
	[[NegotiationStatus]] Script
-------------------------------------------------*/

IF NOT EXISTS (SELECT * FROM [dbo].[NegotiationStatus])
BEGIN

BEGIN TRANSACTION;
INSERT INTO [dbo].[NegotiationStatus]([StatusID], [StatusDescription])
SELECT N'e3b0b130-133e-4c1d-957c-14c67869446c', N'Open' UNION ALL
SELECT N'dfcea50d-18a2-4e41-9be8-1673e88101c4', N'Closed'
COMMIT;
RAISERROR (N'[dbo].[NegotiationStatus]: Insert Batch: 1.....Done!', 10, 1) WITH NOWAIT;
END
GO

 
/*-----------------------------------------------
	[Right] Script
-------------------------------------------------*/

IF NOT EXISTS (SELECT * FROM [dbo].[Right])
BEGIN
BEGIN TRANSACTION;
INSERT INTO [dbo].[Right]([RightID], [RightName], [RightDescription])
SELECT N'ab492d77-7486-4207-bdc4-0169b50fabfa', N'Conversations_Delete', N'Delete an Existing Conversation' UNION ALL
SELECT N'ef791004-8e25-4bd4-9814-0e2200cff798', N'Manage_Apps_See', N'View Apps' UNION ALL
SELECT N'ffdae97b-8684-48b3-8ade-0e30d43eb39a', N'Negotiations_Start', N'Add New Negotiation' UNION ALL
SELECT N'5596b358-5b6c-4c36-9b91-1bbc74fb7294', N'Conversations_See', N'View or browse the Conversation' UNION ALL
SELECT N'd0d754a4-6832-4e65-82b4-2474238e6e7c', N'Negotiations_Delete', N'Delete an Existing Negotiation' UNION ALL
SELECT N'e7a3208c-18f6-4360-b464-32662c8d486c', N'Negotiations_See', N'View or browse the Negotiation' UNION ALL
SELECT N'564870f5-d47a-4cb2-985b-39ac36d45a30', N'Manage_Apps_Configure', N'Configure The Apps' UNION ALL
SELECT N'c162370b-815e-4b77-969d-52cb26c8b001', N'Negotiations_Re_Open', N'Re-Open an Existing Negotiation' UNION ALL
SELECT N'2826fea0-cfe2-49d8-a86f-6f1f718988e6', N'Conversations_Start', N'Add New Conversation' UNION ALL
SELECT N'3a780288-2390-4b65-a791-70ef81bca820', N'Manage_Apps_Activate_Deactivate', N'Activate De-Active Apps' UNION ALL
SELECT N'b15f88ca-249c-44b0-be03-7971ba06005d', N'Conversations_Rename', N'Rename an Existing Conversation' UNION ALL
SELECT N'633a9eb1-1d1d-41ad-9779-991583a01de6', N'Negotiations_Close', N'Close an Existing Negotiation' UNION ALL
SELECT N'01b00612-5c92-479e-883c-b9a8234d615b', N'Negotiations_Rename', N'Rename an Existing Negotiation'
COMMIT;
RAISERROR (N'[dbo].[Right]: Insert Batch: 1.....Done!', 10, 1) WITH NOWAIT;
END
GO

 
/*-----------------------------------------------
	[[Role]] Script
-------------------------------------------------*/

IF NOT EXISTS (SELECT * FROM [dbo].[Role])
BEGIN
BEGIN TRANSACTION;
INSERT INTO [dbo].[Role]([RoleID], [RoleName], [RoleDescription])
SELECT N'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa', N'Admin', N'for Admin Users' UNION ALL
SELECT N'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaab', N'User', N'for Normal Users'
COMMIT;
RAISERROR (N'[dbo].[Role]: Insert Batch: 1.....Done!', 10, 1) WITH NOWAIT;
END
GO

/*-----------------------------------------------
	[[Channel]] Script
-------------------------------------------------*/

IF NOT EXISTS (SELECT * FROM [dbo].[Channel])
BEGIN

BEGIN TRANSACTION;
INSERT INTO [dbo].[Channel]([ChannelID], [ChannelName], [ChannelTypeID])
SELECT N'e3f62f4f-0050-4dd2-a92b-ddb6fbecc041', N'Email'        , N'7529415a-bcc7-448b-815e-af522a504ed7' UNION ALL
SELECT N'af97ff15-729d-4fe1-a507-5057de8a7f68', N'Messenger', N'7529415a-bcc7-448b-815e-af522a504ed7' UNION ALL
SELECT N'44125bd1-8eaf-491a-bdb1-5dc6b6d31024', N'Phone'          , N'7529415a-bcc7-448b-815e-af522a504ed7' UNION ALL
SELECT N'10C82217-C163-4C30-B344-1A81256C6A23', N'Preference App' , N'01862435-6783-43D3-B454-2DF645EB2875' UNION ALL
SELECT N'D38C8FF7-D84C-4401-A66D-FAE3399A04CD', N'eSource App'    , N'01862435-6783-43D3-B454-2DF645EB2875' UNION ALL
SELECT N'24216B75-21D1-458D-A5D0-845B3FD5C90A', N'Culture App'    , N'01862435-6783-43D3-B454-2DF645EB2875'
COMMIT;
RAISERROR (N'[dbo].[Channel]: Insert Batch: 1.....Done!', 10, 1) WITH NOWAIT;
END

GO


/*-----------------------------------------------
	RoleRight Script
-------------------------------------------------*/

IF NOT EXISTS (SELECT * FROM [dbo].RoleRight)
BEGIN

BEGIN TRANSACTION;
Insert Into dbo.RoleRight(RoleRightID,RightID,RoleID)
Select NEWID(),RightID,RoleID from dbo.[Right] ,[Role]
COMMIT;
RAISERROR (N'[dbo].[RoleRight]: Insert Batch: 1.....Done!', 10, 1) WITH NOWAIT;
END
go


/*-----------------------------------------------
	Application Script
-------------------------------------------------*/

IF NOT EXISTS (SELECT * FROM [dbo].[Application])
BEGIN

	BEGIN TRANSACTION;
	INSERT INTO [dbo].[Application]([ApplicationID], [ApplicationRank], [ApplicationTitle], [ApplicationBaseAddress], [ApplicationMainServicePath], [AssemblyName], [ModuleName], [XapFilePath])
	SELECT N'5f7a27a8-5083-4e60-be81-75449376252f', 1, N'Preference App', N'http://localhost:9002/', N'http://localhost:9002/citPOINT-prefApp-Data-Web-PrefAppService.svc', N'citPOINT.PreferenceApp.Client, PreferenceAppModule, Version=1.0.0.0, Culture =neutral, PublicKeyToken=null', N'PreferenceAppModule', N'http://localhost:9002/clientbin/citPOINT.PrefApp.Client.xap' UNION ALL
	SELECT N'0ce3da0a-43b8-4393-83ea-0556d9bf42c5', 2, N'Culture App', N'http://localhost:9003/', N'http://localhost:9003/citPOINT-CultureApp-Data-Web-CultureAppService.svc', N'citPOINT.CultureApp.Client, CultureAppModuleModule, Version=1.0.0.0, Culture =neutral, PublicKeyToken=null', N'CultureAppModule', N'http://localhost:9003/clientbin/citPOINT.CultureApp.Client.xap' UNION ALL
	SELECT N'67cb25f2-acbd-4b2b-9917-ba5ea62150fe', 3, N'eSource App', N'http://localhost:9007/', N'http://localhost:9007/citPOINT-eSourceApp-Data-Web-eSourceAppService.svc', N'citPOINT.eSourceApp.Client, eSourceAppModule, Version=1.0.0.0, Culture =neutral, PublicKeyToken=null', N'eSourceAppModule', N'http://localhost:9007/clientbin/citPOINT.eSourceApp.Client.xap' UNION ALL
	SELECT N'8b1ec7fa-5f2f-4485-be31-21cc9cecdbb9', 4, N'Issue App', N'http://localhost:9005/', N'http://localhost:9005/citPOINT-IssueApp-Data-Web-IssueAppService.svc', N'citPOINT.IssueApp.Client, IssueAppModule, Version=1.0.0.0, Culture =neutral, PublicKeyToken=null', N'IssueAppModule', N'http://localhost:9005/clientbin/citPOINT.IssueApp.Client.xap' UNION ALL
	SELECT N'e4d51b5c-2d73-4f90-93a0-8364761a9930', 5, N'Message App', N'http://localhost:9004/', N'http://localhost:9004/citPOINT-MessageApp-Data-Web-MessageAppService.svc', N'citPOINT.MessageApp.Client, MessageAppModule, Version=1.0.0.0, Culture =neutral, PublicKeyToken=null', N'MessageAppModule', N'http://localhost:9004/clientbin/citPOINT.MessageApp.Client.xap' UNION ALL
	SELECT N'1276b329-6e41-4b5d-88d5-cebdb027c60d', 6, N'Offer App', N'http://localhost:9008/', N'http://localhost:9008/citPOINT-OfferApp-Data-Web-OfferAppService.svc', N'citPOINT.OfferApp.Client, OfferAppModuleModule, Version=1.0.0.0, Culture =neutral, PublicKeyToken=null', N'OfferAppModule', N'http://localhost:9008/clientbin/citPOINT.OfferApp.Client.xap' UNION ALL
	SELECT N'8b1ec7fa-5f2f-4485-be31-21cc9cecd0b0', 7, N'Strategy App', N'http://localhost:9006/', N'http://localhost:9006/citPOINT-StrategyApp-Data-Web-StrategyAppService.svc', N'citPOINT.StrategyApp.Client, StrategyAppModule, Version=1.0.0.0, Culture =neutral, PublicKeyToken=null', N'StrategyAppModule', N'http://localhost:9006/clientbin/citPOINT.StrategyApp.Client.xap'
	COMMIT;
	RAISERROR (N'[dbo].[Application]: Insert Batch: 1.....Done!', 10, 1) WITH NOWAIT;

END
GO
 

/*-----------------------------------------------
	Culture Script
-------------------------------------------------*/

IF NOT EXISTS (SELECT * FROM [dbo].[Culture])
BEGIN

	BEGIN TRANSACTION;
	INSERT INTO [dbo].[Culture]([CultureID], [CultureName])
	SELECT 1, N'Arab World' UNION ALL
	SELECT 2, N'Argentina' UNION ALL
	SELECT 3, N'Australia' UNION ALL
	SELECT 5, N'Austria' UNION ALL
	SELECT 6, N'Bangladesh' UNION ALL
	SELECT 7, N'Belgium' UNION ALL
	SELECT 8, N'Brazil' UNION ALL
	SELECT 9, N'Bulgaria' UNION ALL
	SELECT 10, N'Canada' UNION ALL
	SELECT 11, N'Chile' UNION ALL
	SELECT 12, N'China' UNION ALL
	SELECT 13, N'Colombia' UNION ALL
	SELECT 14, N'Costa Rica' UNION ALL
	SELECT 15, N'Czech Republic' UNION ALL
	SELECT 16, N'Denmark' UNION ALL
	SELECT 17, N'East Africa' UNION ALL
	SELECT 18, N'Ecuador' UNION ALL
	SELECT 19, N'El Salvador' UNION ALL
	SELECT 20, N'Estonia' UNION ALL
	SELECT 21, N'Finland' UNION ALL
	SELECT 22, N'France' UNION ALL
	SELECT 23, N'Germany' UNION ALL
	SELECT 24, N'Greece' UNION ALL
	SELECT 25, N'Guatemala' UNION ALL
	SELECT 26, N'Hong Kong' UNION ALL
	SELECT 27, N'Hungary' UNION ALL
	SELECT 28, N'India' UNION ALL
	SELECT 29, N'Indonesia' UNION ALL
	SELECT 30, N'Iran' UNION ALL
	SELECT 31, N'Ireland' UNION ALL
	SELECT 32, N'Israel' UNION ALL
	SELECT 33, N'Italy' UNION ALL
	SELECT 34, N'Jamaica' UNION ALL
	SELECT 35, N'Japan' UNION ALL
	SELECT 36, N'Luxembourg' UNION ALL
	SELECT 37, N'Malaysia' UNION ALL
	SELECT 38, N'Malta' UNION ALL
	SELECT 39, N'Mexico' UNION ALL
	SELECT 40, N'Morocco' UNION ALL
	SELECT 41, N'Netherlands' UNION ALL
	SELECT 42, N'New Zealand' UNION ALL
	SELECT 43, N'Norway' UNION ALL
	SELECT 44, N'Pakistan' UNION ALL
	SELECT 45, N'Panama' UNION ALL
	SELECT 46, N'Peru' UNION ALL
	SELECT 47, N'Philippines' UNION ALL
	SELECT 48, N'Poland' UNION ALL
	SELECT 49, N'Portugal' UNION ALL
	SELECT 50, N'Romania' UNION ALL
	SELECT 51, N'Russia' UNION ALL
	SELECT 52, N'Singapore' UNION ALL
	SELECT 53, N'Slovakia' UNION ALL
	SELECT 54, N'South Africa' UNION ALL
	SELECT 55, N'South Korea' UNION ALL
	SELECT 56, N'Spain' UNION ALL
	SELECT 57, N'Surinam' UNION ALL
	SELECT 58, N'Sweden' UNION ALL
	SELECT 59, N'Switzerland' UNION ALL
	SELECT 60, N'Taiwan' UNION ALL
	SELECT 61, N'Thailand' UNION ALL
	SELECT 62, N'Trinidad' UNION ALL
	SELECT 63, N'Turkey' UNION ALL
	SELECT 64, N'United Kingdom' UNION ALL
	SELECT 65, N'United States' UNION ALL
	SELECT 66, N'Uruguay' UNION ALL
	SELECT 67, N'Venezuela' UNION ALL
	SELECT 68, N'Vietnam' UNION ALL
	SELECT 69, N'West Africa'
	COMMIT;
RAISERROR (N'[dbo].[Culture]: Insert Batch: 1.....Done!', 10, 1) WITH NOWAIT;
END
GO


/*-----------------------------------------------
	Organization Type Script
-------------------------------------------------*/

IF NOT EXISTS (SELECT * FROM [dbo].[OrganizationType])
BEGIN

BEGIN TRANSACTION;

INSERT INTO [dbo].[OrganizationType]([OrganizationTypeID], [OrganizationTypeName])
SELECT 1, N'Sole Proprietorship - EU' UNION ALL
SELECT 2, N'Partnership - OG' UNION ALL
SELECT 3, N'LP - KG' UNION ALL
SELECT 4, N'LLC/LTD – GmbH' UNION ALL
SELECT 5, N'PLC – AG' UNION ALL
SELECT 6, N'NGO - NGO'

COMMIT;
RAISERROR (N'[dbo].[OrganizationType]: Insert Batch: 1.....Done!', 10, 1) WITH NOWAIT;
END
go

 
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N'eNegUser')
DROP USER [eNegUser]
GO

/****** Object:  Login [eNegUser]    Script Date: 08/25/2010 10:31:45 ******/
IF  EXISTS (SELECT * FROM sys.server_principals WHERE name = N'eNegUser')
DROP LOGIN [eNegUser]
GO

/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [eNegUser]    Script Date: 08/25/2010 10:31:45 ******/
CREATE LOGIN [eNegUser] WITH PASSWORD='eNegUserPassword', DEFAULT_DATABASE=[eNeg], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

 
CREATE USER [eNegUser] FOR LOGIN [eNegUser] 
GO


EXEC sp_addrolemember N'db_owner', N'eNegUser'
go