import { PageMetaModel } from './page-meta.model';

export const PAGE_META_DEFAULT_TITLE = 'Architectural Studio Tradition';
export const PAGE_META_DEFAULT_IMAGE = '';

export const PAGE_META: { [key: string]: PageMetaModel; } = {
  HOME: {
    title: PAGE_META_DEFAULT_TITLE,
    description: 'Architectural Studio Tradition',
    imageUrl: PAGE_META_DEFAULT_IMAGE,
    url: null,
  },
  SEARCH: {
    title: `Search Results`,
    description: 'Search Results',
    imageUrl: PAGE_META_DEFAULT_IMAGE,
    url: null,
  },
  ABOUT: {
    title: `About the Studio`,
    description: 'About the Studio',
    imageUrl: PAGE_META_DEFAULT_IMAGE,
    url: null,
  },
  NOT_FOUND: {
    title: 'Not found',
    description: 'Requested page can not be found',
    imageUrl: PAGE_META_DEFAULT_IMAGE,
    url: null,
  },
  CONTACT_US: {
    title: 'Contact us',
    description: 'Contact us',
    imageUrl: PAGE_META_DEFAULT_IMAGE,
    url: null,
  },
  TERMS_AND_CONDITIONS: {
    title: 'Terms and Conditions',
    description: 'Terms and Conditions',
    imageUrl: PAGE_META_DEFAULT_IMAGE,
    url: null,
  },
};
