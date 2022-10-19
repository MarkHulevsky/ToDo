// This file can be replaced during build by using the `fileReplacements` array.
// `ng build` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  gatewayUrl: 'https://localhost:7022/',
  identityConfiguration: {
    clientId: 'authorized_client_id',
    scope: 'openid offline_access TodoAPI MailAPI UsersAPI DocumentAPI',
    tokenEndpoint: 'https://localhost:7007/connect/token',
    userInfoEndpoint: 'https://localhost:7007/connect/userinfo',
  }
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/plugins/zone-error';  // Included with Angular CLI.